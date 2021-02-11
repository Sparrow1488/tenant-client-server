using System;
using System.Collections.Generic;
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
                await Task.Run(() =>
                {
                    GetUsers();
                });
            }
        }
        public static void GetUsers()
        {
            var getClient = listener.AcceptTcpClient();
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
            var stream = client.GetStream();
            var data = new byte[256];
            var response = new StringBuilder();
            int bytes;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                response.Append(Encoding.UTF8.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);
            Console.WriteLine("Сообщение:" + response.ToString());
            stream.Close();
            chat.Add(response.ToString());

            if (connectedUsers != null)
            {
                for (int i = 0; i < connectedUsers.Count - 1; i++)
                {
                    var userStream = connectedUsers[i].GetStream();
                    userStream.Write(data, 0, data.Length);
                    Console.WriteLine("Сообщение отправлено");
                    userStream.Flush();
                    userStream.Close();
                }
            }
            else
                Console.WriteLine("Нет подключенных пользователей");
        }
    }
}
