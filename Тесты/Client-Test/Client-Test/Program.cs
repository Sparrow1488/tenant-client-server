using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client_Test
{
    class Program
    {
        static TcpClient client = new TcpClient();
        static IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        static async Task Main(string[] args)
        {
            Console.WriteLine("App start");
            client.Connect(serverEndPoint);
            NetworkStream stream = client.GetStream();

            string request = "Client is connected!";
            byte[] data = Encoding.UTF8.GetBytes(request);
            await stream.WriteAsync(data, 0, data.Length);
            Console.WriteLine("Write in " +  stream.WriteTimeout);

            stream.Close();
            client.Close();
            Console.WriteLine("До свзяи...");

        }
    }
}
