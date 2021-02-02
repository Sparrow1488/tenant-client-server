using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Multi_Server_Test.Server.Packages;
using Newtonsoft.Json;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Blocks.Auth;

namespace Multi_Server_Test
{
    class Program
    {
        private static void CreateServerBlocks()
        {
            new AuthorizationBlock("auth");
        }
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8090);
            server.Start();
            Console.WriteLine($"Server started: {DateTime.Now}");

            CreateServerBlocks();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Active blocks:");
            foreach (var block in ServerBlock.ExistsServerBlocks)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" > " + block.BlockAction);
                Console.ForegroundColor = ConsoleColor.White;
            }

            while (true)
            {
                Console.WriteLine("Ожидаю запроса.......");
                var client = server.AcceptTcpClient();
                Console.WriteLine("Client connect");

                var clientStream = client.GetStream();
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

                ////TODO: сделать маршрутизатор запроса
                Router requestRoute = new Router(getPackage.SendingMeta.Action, clientObject, clientStream);
                await requestRoute.CompleteRouteAsync();
                
                Console.WriteLine("Request processed");
                Console.WriteLine();
            }
            
        }
    }
    
}

