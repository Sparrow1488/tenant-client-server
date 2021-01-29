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

            using (StreamWriter file = File.CreateText("package.json"))
            {
                JsonSerializer serializer = JsonSerializer.Create(settings);
                serializer.Serialize(file, package);
            }
            using (StreamReader file = File.OpenText("package.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                var str = file.ReadToEnd();
                var obj = JsonConvert.DeserializeObject<Package>(str, settings);
                Console.WriteLine(obj.Meta.StringMeta);
                var getPerson = JsonConvert.SerializeObject(obj.Pack);
                var desPerson = JsonConvert.DeserializeObject<Person>(getPerson);
                Console.WriteLine(desPerson.MyName);
            }





            //Console.ForegroundColor = ConsoleColor.DarkMagenta;
            //Console.WriteLine("Server launch");
            //Console.ForegroundColor = ConsoleColor.White;

            //ServerPorts server_8090 = new ServerPorts(ip, 8090, "server90");
            //ServerPorts server_8080 = new ServerPorts(ip, 8080, "server80");

            //while (true)
            //{
            //    await WriteCommands();
            //}

        }
        public static async Task WriteCommands()
        {
            await Task.Run(() =>
            {
                Console.Write(">");
                string command = Console.ReadLine();
            });
        }
    }
}
