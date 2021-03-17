using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Blocks.Auth;
using Multi_Server_Test.Server.Blocks.LetterBlock;
using Multi_Server_Test.Server.Controllers;
using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.Server.Models.AuthBlock;
using Multi_Server_Test.Server.Models.LetterBlock;
using Multi_Server_Test.Server.Models.NewsBlock;
using Multi_Server_Test.Server.Models.SourceBlock;
using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Blocks.Auth;
using Multi_Server_Test.ServerData.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Server_Test.ServerData
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
        private Synchronizator synchronizator = new Synchronizator();
        private ServerReportsModule modulEvents = new ServerReportsModule();
        #endregion

        #region ServerMethods
        public async void Start()
        {
            Listener.Start();

            var allUsersOutDB = functions.GetAllUsersOutDB();
            allUsers = await synchronizator.SynchronizeCollection(allUsersOutDB, Meta.reservePath, Meta.reserveUsersTxt);
            Console.WriteLine("Пользователей синхронизированно: " + allUsers.Count);

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
            var validClient = tcpClient as TcpClient;
            if (validClient.Connected)
            {
                modulEvents.BlockReport(this, "Client connect", ConsoleColor.Green);

                var clientStream = validClient.GetStream();
                string jsonPackage = GetDataFromStream(clientStream);
                if (jsonPackage == null)
                {
                    modulEvents.BlockReport(this, "Ошибка: поток не поддерживает чтение", ConsoleColor.Red);
                    return;
                }

                if (!string.IsNullOrWhiteSpace(jsonPackage.ToString()))
                {
                    var getPackage = JsonConvert.DeserializeObject<Package>(jsonPackage.ToString());
                    Console.WriteLine(getPackage.SendingMeta);
                    var clientObject = JsonConvert.SerializeObject(getPackage.SendingObject);

                    modulEvents.BlockReport(this, "Distribute request to handle in main routing controller...", ConsoleColor.Yellow);

                    var letterModels = new List<Model>()
                    {
                        new SendLetterModel("send", false),
                        new GetAllLettersModel("get-all", true),
                        new LettersReplyModel("reply", false),
                        new GetReplyOnLetterModel("get-reply", true),
                        new GetMyLettersModel("get-my", false)
                    };

                    var userModels = new List<Model>()
                    {
                        new AuthorizationModel("auth", false),
                        new AuthorizationForTokenModel("auth-token", false)
                    };

                    var newsModels = new List<Model>()
                    {
                        new GetNewsCollectionModel("get", false),
                        new AddNewsModel("add", true)
                    };

                    var sourceModels = new List<Model>()
                    {
                        new AddSourceModel("add", false),
                        new GetSourceModel("get-token", false)
                    };

                    List<Controller> newControllers = new List<Controller>()
                    {
                        new UserController("User", userModels),
                        new NewsController("News", newsModels),
                        new LettersController("Letter", letterModels),
                        new SourceController("Source", sourceModels)
                    };

                    MainRouter router = new MainRouter(newControllers);
                    router.ExecuteRouting(getPackage, validClient);
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
                    int bytes = clientStream.Read(buffer, 0, buffer.Length);
                    jsonPackage.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
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
        #endregion
    }
}
