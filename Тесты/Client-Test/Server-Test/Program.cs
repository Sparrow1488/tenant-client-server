using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server_Test
{
    public class Program
    {
        static IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8090);
        static TcpListener server = new TcpListener(localEndPoint);
        static async Task Main(string[] args)
        {
            Console.ReadKey();
            Console.WriteLine("App started");

            server.Start();
            Console.WriteLine("Server started");
            

            while (true)
            {
                Console.WriteLine("Wait for connecting...");
                TcpClient getClient = server.AcceptTcpClient();
                Console.WriteLine("Client connecting");

                NetworkStream stream = getClient.GetStream();
                byte[] getData = new byte[1024];
                StringBuilder response = new StringBuilder();

                if (stream.CanRead)
                {
                    do
                    {
                        int bytes = stream.Read(getData, 0, getData.Length);
                        response.Append(Encoding.UTF8.GetString(getData, 0, bytes));
                    } 
                    while (stream.DataAvailable);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Get data: " + response);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine("Get stream can not read");
                }

                byte[] sendData = Encoding.UTF8.GetBytes("Ответ сервера");
                await stream.WriteAsync(sendData, 0, sendData.Length);
                Console.WriteLine("Write data in " + stream.WriteTimeout);

                stream.Close();
            }
            server.Stop();
            Console.WriteLine("Server stop");
        }
    }
}
