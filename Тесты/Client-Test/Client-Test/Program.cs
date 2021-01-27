using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Client_Test.Client;
using Client_Test.Blocks.Auth;

namespace Client_Test
{
    class Program
    {
        //НА КЛИЕНТЕ НЕЛЬЗЯ ОДНОВРЕМЕННО ИСПОЛЬЗОВАТЬ И TcpClient и TcpListener
        static TcpClient client = new TcpClient();
        //static TcpListener clientListener = new TcpListener(serverEndPoint);
        static IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8090);
        static async Task Main(string[] args)
        {
            Console.ReadKey();
            Console.WriteLine("Client start");
            client.Connect(serverEndPoint);

            NetworkStream stream = client.GetStream();

            var packageData = CreateRequest();

            string request = JsonConvert.SerializeObject(packageData);
            byte[] data = Encoding.UTF8.GetBytes(request);
            await stream.WriteAsync(data, 0, data.Length);
            Console.WriteLine("Write in " +  stream.WriteTimeout);

            Console.WriteLine("Wait for answer....");
            var getStream = client.GetStream();
            if (getStream.CanRead)
            {
                StringBuilder response = new StringBuilder();
                byte[] getData = new byte[1024];
                do
                {
                    int bytes = getStream.Read(getData, 0, getData.Length);
                    response.Append(Encoding.UTF8.GetString(getData, 0, bytes));
                }
                while (getStream.DataAvailable);
                Console.WriteLine("Get answer: " + response);
            }
            else
            {
                Console.WriteLine("Stream can not read");
            }
            stream.Close();
            client.Close();
            Console.WriteLine("До свзяи...");
            Console.ReadLine();
        }
        private static PersonRequest CreateRequest()
        {
            var person = new Person()
            {
                Name = "Valentin",
                ID = 24
            };
            var meta = new Meta()
            {
                Address = "127.0.0.1",
                Action = "Auth"
            };
            return new PersonRequest()
            {
                DataPerson = person,
                DataMeta = meta
            };
        }
    }
}
