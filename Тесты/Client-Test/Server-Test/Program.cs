using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server_Test
{
    public class Program
    {
        static IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        static TcpListener server = new TcpListener(localEndPoint);
        static async Task Main(string[] args)
        {
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
            }
        }
    }
}
