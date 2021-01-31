using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server_Test
{
    public class Program
    {
        static string ip = "127.0.0.1";
        static async Task Main(string[] args)
        {
            var person = new Person() { MyName = "Name"};
            var meta = new MetaClass("myMetaName");
            var package = new SendPack(person, meta);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            ServerPorts server_8080 = new ServerPorts(ip, 8080, "server80");

        }
    }
}
