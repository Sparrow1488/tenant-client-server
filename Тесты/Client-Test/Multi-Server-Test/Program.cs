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

            ////<<---!!! НАСТРОЙКИ НЕОБХОДИМЫ !!!--->>

            //var stringObj = JsonConvert.SerializeObject(package, JsonSettings);
            //var obj = JsonConvert.DeserializeObject<Package>(stringObj, JsonSettings);
            //Console.WriteLine(obj.SendingMeta.Action);

            //var personString = JsonConvert.SerializeObject(obj.SendingObject, JsonSettings);
            //var personObj = JsonConvert.DeserializeObject<Person>(personString, JsonSettings);
            //Console.WriteLine(personObj.Name);

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
            // "$type": "WpfApp1.Server.UserInfo.Test, WpfApp1"
            //\"$type\": \"WpfApp1.Classes.Client.Requests.PersonRequest, WpfApp1\"
            jsonPackage = jsonPackage.Replace("\"WpfApp1.Classes.Client.Requests.PersonRequest, WpfApp1\"", "\"Multi_Server_Test.Server.Requests.PersonRequest, Multi_Server_Test\"");
            var getPackage = JsonConvert.DeserializeObject<Package>(jsonPackage.ToString(), JsonSettings);
            //Console.WriteLine("Получена мета: {0}, {1}", getPackage.SendingMeta.Address, getPackage.SendingMeta.Action);

            ////TODO: сделать маршрутизатор запроса
            //if (getPackage.SendingMeta.Action.Equals("auth"))
            //{
            //    var person = (Person)getPackage.SendingObject;
            //    Console.WriteLine("Получен пользователь: {0}", person.Login);
            //}
        }
    }
    
}

