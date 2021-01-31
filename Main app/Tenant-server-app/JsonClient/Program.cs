using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using JsonClient.API;
using Newtonsoft.Json;

namespace JsonClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            //client.Connect(IPAddress.Parse("127.0.0.1"), 8080);
            //Console.WriteLine("Connect");
            //var stream = client.GetStream();

            var data = new byte[1024];

            var person = new Person("Valentin", "1488");
            //var getObj = JsonConvert.DeserializeObject<Package<Person>>(json);
            //Console.WriteLine(getObj.SendObject.Name);

            //data = Encoding.UTF8.GetBytes(json);

            //stream.Write(data, 0, data.Length);
            Console.WriteLine("отправлено");
        }
        const string META = "auth"; //команды будут в будущем в отдельной коллекции
        public Person Authorization(Person inputPerson)
        {
            var meta = new Meta(META);
            var pack = new Package<Person>(inputPerson, meta);
            var jsonPack = JsonConvert.SerializeObject(pack);
            return null;
        }
    }
}
