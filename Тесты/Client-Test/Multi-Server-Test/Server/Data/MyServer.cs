using FireSharp;
using FireSharp.Response;
using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Blocks.Auth;
using Multi_Server_Test.Server.Blocks.LetterBlock;
using Multi_Server_Test.Server.Controllers;
using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.Server.Models.AuthBlock;
using Multi_Server_Test.Server.Models.LetterBlock;
using Multi_Server_Test.Server.Models.NewsBlock;
using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Blocks.Auth;
using Multi_Server_Test.ServerData.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Server_Test.ServerData
{
    public class MyServer
    {
        public string HOST = null;
        public int PORT = 0;
        public string serverName { get; }

        public TcpListener Listener = null;
        public static ServerMeta Meta = null;

        public static List<News> newsCollectionOutDB = null;
        public static List<Letter> allLetters = null;
        public static Dictionary<UserToken, Person> tokensDictionary = new Dictionary<UserToken, Person>();
        public MyServer(string host, int port)
        {
            if (!string.IsNullOrWhiteSpace(host) && port > 10)
            {
                HOST = host;
                PORT = port;
                serverName = "JumboServer";

                Listener = new TcpListener(IPAddress.Parse(HOST), PORT);
                Meta = new ServerMeta();
            }
            else
            {
                throw new ArgumentException("Вы передали некорректные значения");
            }
        }

        private ServerFunctions functions = new ServerFunctions();
        private Synchronizator synchronizator = new Synchronizator();
        private ServerReportsModule modulEvents = new ServerReportsModule();
        public async void Start()
        {
            Listener.Start();
            var collectionOutDB = functions.GetAllNewsOutDB();
            newsCollectionOutDB = await synchronizator.SynchronizeCollection(collectionOutDB, Meta.newsPath, Meta.reserveNewsCollection);
            foreach (var news in newsCollectionOutDB)
                Console.WriteLine(news.Title + "\n");

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
                if(jsonPackage == null)
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
                        new SendLetterModel("send"),
                        new GetLetterModel("get"),
                        new LettersReplyModel("reply")
                    };

                    var userModels = new List<Model>()
                    { 
                        new AuthorizationModel("auth"),
                        new AuthorizationForTokenModel("auth-token")
                    };

                    var newsModels = new List<Model>()
                    { 
                        new GetNewsCollectionModel("get"),
                        new AddNewsModel("add")
                    };

                    List<Controller> newControllers = new List<Controller>()
                    {
                        new UserController("User", userModels),
                        new NewsController("News", newsModels),
                        new LettersController("Letter", letterModels)
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
        
        
        #region Вторичные методы
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
        #endregion
    }
}
