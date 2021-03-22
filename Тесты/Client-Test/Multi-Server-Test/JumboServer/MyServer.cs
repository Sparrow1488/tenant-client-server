using JumboServer.API;
using JumboServer.Controllers;
using JumboServer.Functions;
using JumboServer.Meta;
using JumboServer.Models;
using JumboServer.Models.Authorization;
using JumboServer.Models.LetterBlock.ADD;
using JumboServer.Models.LetterBlock.GET;
using JumboServer.Models.NewsBlock.ADD;
using JumboServer.Models.NewsBlock.GET;
using JumboServer.Models.SourceBlock.ADD;
using JumboServer.Models.SourceBlock.GET;
using JumboServer.Packages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JumboServer
{
    public class MyServer
    {
        #region Constructor
        public MyServer(string host, int port)
        {
            if (!string.IsNullOrWhiteSpace(host) && port > 10)
            {
                HOST = host;
                PORT = port;

                Listener = new TcpListener(IPAddress.Parse(HOST), PORT);
                Meta = new ServerMeta();
            }
            else
            {
                throw new ArgumentException("Вы передали некорректные значения");
            }
        }
        #endregion

        #region Props
        public string HOST = null;
        public int PORT = 0;
        public string serverName { get; } = "JumboServer";

        public TcpListener Listener = null;
        public static ServerMeta Meta = null;
        public MainRouter MainRouter;
        #endregion

        #region LocalStorage
        public static List<News> allNews = null;
        public static List<Letter> allLetters = null;
        public static List<Person> allUsers = null;

        public static List<News> noSynchNews = new List<News>();
        public static List<Letter> noSynchLetters = new List<Letter>();
        public static List<Person> noSynchUsers = new List<Person>();

        public static Dictionary<UserToken, Person> tokensDictionary = new Dictionary<UserToken, Person>();
        #endregion

        #region OtherInstances
        private ServerFunctions functions = new ServerFunctions();
        private CollectionSynchronizator synchronizator = new CollectionSynchronizator();
        private ServerReportsModule modulEvents = new ServerReportsModule();
        #endregion

        #region ServerMethods
        public async void Start()
        {
            Listener.Start();

            #region SynchUsers
            var allUsersOutDB = functions.GetAllUsersOutDB();
            allUsers = await synchronizator.SynchronizeCollection(allUsersOutDB, Meta.reservePath, Meta.reserveUsersTxt);
            Console.WriteLine("Пользователей синхронизированно: " + allUsers.Count);
            #endregion

            #region SynchNews
            if (allUsersOutDB != null)
            {
                var allNewsOutDB = functions.GetAllNewsOutDB();
                allNews = await synchronizator.SynchronizeCollection(allNewsOutDB, Meta.reservePath, Meta.reserveNewsCollectionTxt);
                Console.WriteLine("Новостей синхронизированно: " + allNews.Count);
            }
            else
            {
                modulEvents.BlockReport(this, "Новости не могут быть получены, тк таблица с пользователями пуста", ConsoleColor.Red);
            }
            #endregion

            #region SynchLetters
            if (allUsers != null)
            {
                var allLettersOutDB = functions.GetAllLettersOutDB();
                allLetters = await synchronizator.SynchronizeCollection(allLettersOutDB, Meta.reservePath, Meta.reserveLettersTxt);
                Console.WriteLine("Писем синхронизированно: " + allLetters.Count);
            }
            else
            {
                modulEvents.BlockReport(this, "Письма не могут быть получены, поскольку таблица с отправителями пуста!", ConsoleColor.Red);
            }
            #endregion

            MainRouter = CreateDefaultMVC();

            modulEvents.BlockReport(this, "Server started!", ConsoleColor.Green);
        }
        public void ServeAndResponseToClient()
        {
            while (true)
            {
                modulEvents.BlockReport(this, "Ожидаю запроса.......", ConsoleColor.White);
                var client = Listener.AcceptTcpClient();
                Task.Factory.StartNew(() => ResponseToClient(client));
            }
        }
        private void ResponseToClient(object tcpClient)
        {
            var connectedClient = tcpClient as TcpClient;
            if (connectedClient.Connected)
            {
                modulEvents.BlockReport(this, "Client connect", ConsoleColor.Green);

                var clientStream = connectedClient.GetStream();
                string jsonPackage = GetDataFromStream(clientStream);
                if (!string.IsNullOrWhiteSpace(jsonPackage.ToString()))
                {
                    var getPackage = JsonConvert.DeserializeObject<Package>(jsonPackage.ToString());
                    Console.WriteLine(getPackage.SendingMeta);
                    var clientObject = JsonConvert.SerializeObject(getPackage.SendingObject);

                    modulEvents.BlockReport(this, "Distribute request to handle in main routing controller...", ConsoleColor.Yellow);
                    MainRouter.ExecuteRouting(getPackage, connectedClient);
                }
                else
                    modulEvents.BlockReport(this, "Пакет данных не может быть получен!", ConsoleColor.Red);
            }
            else
                modulEvents.BlockReport(this, "Client not connected", ConsoleColor.Red);

        }
        #endregion

        #region AdditionalMethods
        private string GetDataFromStream(NetworkStream clientStream)
        {
            if (clientStream.CanRead)
            {
                byte[] buffer = new byte[1024];
                StringBuilder jsonPackage = new StringBuilder();
                do
                {
                    try 
                    {
                        int bytes = clientStream.Read(buffer, 0, buffer.Length);
                        jsonPackage.Append(ServerMeta.Encoding.GetString(buffer, 0, bytes));
                    }
                    catch (IOException) { }
                }
                while (clientStream.DataAvailable);
                return jsonPackage.ToString();
            }
            else
                return null; 
        }
        public static void AddLetterInLocalStorage(Letter letter)
        {
            var funcs = new ServerFunctions();

            if(letter.SourcesTokens != null)
            {
                var existLetterAttachs = funcs.ReturnExistTokens(letter.SourcesTokens);
                letter.SourcesTokens = existLetterAttachs;
            }
            allLetters.Add(letter);
            noSynchLetters.Add(letter);
        }

        public MainRouter CreateDefaultMVC()
        {
            #region CreatingModels

            #region Letters
            var letterModels = new List<Model>()
            {
                new SendLetterModel("send", false),
                new GetAllLettersModel("get-all", true),
                new LettersReplyModel("reply", false),
                new GetReplyOnLetterModel("get-reply", true),
                new GetMyLettersModel("get-my", false)
            };
            #endregion 

            #region Users
            var userModels = new List<Model>()
            {
                new AuthorizationModel("auth", false),
                new AuthorizationForTokenModel("auth-token", false)
            };
            #endregion

            #region News
            var newsModels = new List<Model>()
            {
                new GetNewsCollectionModel("get", false),
                new AddNewsModel("add", true)
            };
            #endregion

            #region Source
            var sourceModels = new List<Model>()
            {
                new AddSourceModel("add", false),
                new GetSourceModel("get-token", false)
            };
            #endregion

            #endregion

            var createdControllers = new List<Controller>()
            {
                new UserController("User", userModels),
                new NewsController("News", newsModels),
                new LettersController("Letter", letterModels),
                new SourceController("Source", sourceModels)
            };

            return new MainRouter(createdControllers);
        }
        #endregion
    }
}
