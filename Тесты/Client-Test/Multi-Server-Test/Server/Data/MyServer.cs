using FireSharp;
using FireSharp.Response;
using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Server;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multi_Server_Test.ServerData
{
    public class MyServer
    {
        public string HOST = null;
        public int PORT = 0;
        public string serverName { get; }

        public TcpListener Listener = null;
        public ServerMeta Meta = null;
        public BlocksSection Blocks = null;

        public MyServer(string host, int port)
        {
            if(!string.IsNullOrWhiteSpace(host) &&
                port > 10)
            {
                HOST = host;
                PORT = port;
                serverName = "MyServer";

                Listener = new TcpListener(IPAddress.Parse(HOST), PORT);
                Meta = new ServerMeta();
                Blocks = new BlocksSection(this); //передаем объект сервера для возможности использовать его методы
            }
            else
            {
                throw new ArgumentException("Вы передали некорректные значения");
            }
        }
        public async Task AddInDb(Person person)
        {
            if (!person.Equals(null))
            {
                await Task.Run(() =>
                {
                    Meta.serverClient.SetAsync($"{Meta.usersPath}/{person.Login}", person);
                });
            }
        }
        public async Task<Person> GetUserOutDataBase(Person person)
        {
            FirebaseResponse respose;
            try
            {
                Meta.serverClient = new FirebaseClient(Meta.firebaseConfig);
                respose = await Meta.serverClient.GetAsync($"{Meta.usersPath}/{person.Login}");
            }
            catch (NullReferenceException) { return null; }

            var user = respose.ResultAs<Person>();
            return user;
        }
        public void Start()
        {
            foreach (var block in Blocks.ExistServerBlocks)
            {
                Console.WriteLine(block.BlockAction + " is active");
            }
            Listener.Start();
            ShowReport("Server started!", ConsoleColor.Green);
        }

        public void ReseveAndServeClient()
        {
            while (true)
            {
                Console.WriteLine("Ожидаю запроса.......");
                var client = Listener.AcceptTcpClient();
                Thread handleClient = new Thread(new ParameterizedThreadStart(ResponseToClient));
                handleClient.Start(client);
            }
        }
        private void ResponseToClient(object tcpClient)
        {
            var validClient = tcpClient as TcpClient;
            Console.Write("Client ");
            string statusConnected = "connect";
            ShowReport(statusConnected, ConsoleColor.Green);

            var clientStream = validClient.GetStream();
            byte[] buffer = new byte[1024];
            StringBuilder jsonPackage = new StringBuilder();
            do
            {
                int bytes = clientStream.Read(buffer, 0, buffer.Length);
                jsonPackage.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
            }
            while (clientStream.DataAvailable);
            var getPackage = JsonConvert.DeserializeObject<Package>(jsonPackage.ToString());
            Console.WriteLine("Получена мета: {0}, {1}", getPackage.SendingMeta.Address, getPackage.SendingMeta.Action);
            var clientObject = JsonConvert.SerializeObject(getPackage.SendingObject);

            ShowReport("Request processing...", ConsoleColor.Yellow);
            Router requestRoute = new Router(getPackage.SendingMeta.Action, clientObject, clientStream);
            requestRoute.CompleteRoute(Blocks.ExistServerBlocks);
        }
        private void ShowReport(string report, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(report);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
