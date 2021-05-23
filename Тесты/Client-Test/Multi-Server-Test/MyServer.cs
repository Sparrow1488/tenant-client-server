using JumboServer.API;
using JumboServer.Controllers;
using JumboServer.Functions;
using JumboServer.Meta;
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
            }
            else
                throw new ArgumentException("Вы передали некорректные значения");
        }
        #endregion

        #region Props
        public string HOST = string.Empty;
        public int PORT = 0;
        public readonly string serverName = "JumboServer";

        public TcpListener Listener = null;
        public ServerMeta Meta = null;
        public Router MainRouter;
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
        public async Task StartAsync()
        {
            CreateMeta();
            await SynchUsers();
            await SynchLetters();
            await SynchNews();
            Listener.Start();

            MainRouter = new Router();
            modulEvents.BlockReport(this, "Server started!", ConsoleColor.Green);
        }
        public void ProcessingClients()
        {
            while (true)
            {
                modulEvents.BlockReport(this, "Ожидаю запроса.......", ConsoleColor.White);
                var client = Listener.AcceptTcpClient();
                Task.Factory.StartNew(() => ReplyToClient(client));

                Task.Delay(150).Wait(); // давайте не будем нагружать ЦП, арррррррррр???
            }
        }
        #endregion

        #region AdditionalMethods
        private void CreateMeta()
        {
            Console.WriteLine("Meta creating...");
            Meta = new ServerMeta();
            Console.WriteLine("Meta was created");
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
        private void RecieveAndRouting(string serializeDataPackage, TcpClient connectedClient)
        {
            
            MainRouter.ExecuteRouting(getPackage, connectedClient);
        }
        private async Task SynchUsers()
        {
            var allUsersOutDB = functions.GetAllUsersOutDB();
            allUsers = await synchronizator.SynchronizeCollection(allUsersOutDB, Meta.reservePath, Meta.reserveUsersTxt);
            Console.WriteLine("Пользователей синхронизированно: " + allUsers.Count);
        }
        private async Task SynchLetters()
        {
            if (allUsers != null)
            {
                var allLettersOutDB = functions.GetAllLettersOutDB();
                allLetters = await synchronizator.SynchronizeCollection(allLettersOutDB, Meta.reservePath, Meta.reserveLettersTxt);
                Console.WriteLine("Писем синхронизированно: " + allLetters.Count);
            }
            else
                modulEvents.BlockReport(this, "Письма не могут быть получены, поскольку таблица с отправителями пуста!", ConsoleColor.Red);
        }
        private async Task SynchNews()
        {
            if (allUsers != null)
            {
                var allNewsOutDB = functions.GetAllNewsOutDB();
                allNews = await synchronizator.SynchronizeCollection(allNewsOutDB, Meta.reservePath, Meta.reserveNewsCollectionTxt);
                Console.WriteLine("Новостей синхронизированно: " + allNews.Count);
            }
            else
                modulEvents.BlockReport(this, "Новости не могут быть получены, тк таблица с пользователями пуста", ConsoleColor.Red);
        }
        private void ReplyToClient(object tcpClient)
        {
            var connectedClient = tcpClient as TcpClient;
            if (connectedClient.Connected)
            {
                modulEvents.BlockReport(this, "Client was connected", ConsoleColor.Green);
                var jsonPackage = MainRouter.ReceiveRequest(connectedClient);
                if (!string.IsNullOrWhiteSpace(receivedJsonPackage))
                    RecieveAndRouting(receivedJsonPackage, connectedClient);
                else
                    modulEvents.BlockReport(this, "Data package can not be recieved!", ConsoleColor.Red);
            }
        }
        #endregion
    }
}
