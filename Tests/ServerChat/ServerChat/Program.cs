
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerChat
{
    class Program
    {
        private static List<string> chat = new List<string>();
        private static List<TcpClient> connectedUsers = new List<TcpClient>();
        private static TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
        public static async Task Main(string[] args)
        {
            listener.Start();
            Console.WriteLine("Server started");
            while (true)
            {
                Console.WriteLine("Ожидаю...");
                var getClient = listener.AcceptTcpClient();

                await Task.Factory.StartNew(() => GetUsers(getClient));
                Console.WriteLine("Принял.");
            }
            
        }
        public static void GetUsers(TcpClient getClient)
        {
            connectedUsers.Add(getClient);
            foreach (var user in connectedUsers)
            {
                Console.WriteLine("User is" + user.Connected);
            }

            while (getClient.Connected)
            {
                GetMessages(getClient);
            }
            Console.WriteLine("User отвалился");
        }
        public static void GetMessages(TcpClient client)
        {
            while (client?.Connected == true)
            {
                if (client.Connected == true)
                {
                    var sr = new StreamReader(client.GetStream());
                    var text = sr.ReadLine();
                    Console.WriteLine(text);
                }
                else
                {
                    break;
                }
                Task.Delay(200);
            }
        }
    }
}
