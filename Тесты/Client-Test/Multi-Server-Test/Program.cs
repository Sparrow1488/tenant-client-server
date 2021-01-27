using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace Multi_Server_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8090);

            server.Start();

            Console.WriteLine("Ожидаю запроса.......");
            var client = server.AcceptTcpClient();
            Console.WriteLine("Client connect");

            var stream = client.GetStream();
            byte[] buffer = new byte[1024];
            StringBuilder data = new StringBuilder();

            do
            {
                int bytes = stream.Read(buffer, 0, buffer.Length);
                data.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
            }
            while (stream.DataAvailable);

            var getPackage = JsonConvert.DeserializeObject(data.ToString());
            Console.WriteLine(getPackage);
            Console.ReadLine();
        }
    }
}
