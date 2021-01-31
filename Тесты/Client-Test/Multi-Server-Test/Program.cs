using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Packages;
using Newtonsoft.Json;

namespace Multi_Server_Test
{
    class Program
    {
        private static JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            //TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto
        };
        static async Task Main(string[] args)
        {
            //var person = new Person();
            //var meta = new SendMeta("AddressName", "auth");
            //var package = new PersonRequest(person, meta);

            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8090);
            server.Start();

            Console.WriteLine("Ожидаю запроса.......");
            var client = server.AcceptTcpClient();
            Console.WriteLine("Client connect");

            var stream = client.GetStream();
            byte[] buffer = new byte[1024];
            StringBuilder jsonPackage = new StringBuilder();
            do
            {
                int bytes = stream.Read(buffer, 0, buffer.Length);
                jsonPackage.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
            }
            while (stream.DataAvailable);
            var getPackage = JsonConvert.DeserializeObject<Package>(jsonPackage.ToString());
            Console.WriteLine("Получена мета: {0}, {1}", getPackage.SendingMeta.Address, getPackage.SendingMeta.Action);

            ////TODO: сделать маршрутизатор запроса
            if (getPackage.SendingMeta.Action.Equals("auth"))
            {
                var jsonPerson = JsonConvert.SerializeObject(getPackage.SendingObject);
                var getPerson = JsonConvert.DeserializeObject<Person>(jsonPerson);

                var response = Encoding.UTF8.GetBytes($"Получен пользователь: {getPerson.Login}");
                await stream.WriteAsync(response, 0, response.Length);
                Console.WriteLine("Ответ отправлен");
            }
        }
    }
    
}

