using System;
using System.Threading.Tasks;
using WpfApp1.Server;
using WpfApp1.Server.ServerMeta;

namespace ConsoleApp1
{
    class Program
    {
        static JumboServer server;
        static async Task Main(string[] args)
        {
            server = new JumboServer(new ServerConfig());
            var authRes = await server.Authorization(new Person("asd", "1234"), true);
            if (!authRes)
                throw new ArgumentException("че");
            for (; ;)
            {
                await Task.Factory.StartNew(GetLetters);
                await Task.Factory.StartNew(GetLetters);
            }

        }
        static async void GetLetters()
        {
            for (; ; )
            {
                var letters = await server.GetMyLetters();
                foreach (var letter in letters)
                    Console.WriteLine(letter.Title);
                Task.Delay(1000).Wait();
            }
        }
        static async void GetNews()
        {
            for (; ; )
            {
                var news = await server.ReceiveNewsCollectionAsync();
                foreach (var newss in news)
                    Console.WriteLine(newss.Title);
                Task.Delay(1000).Wait();
            }
        }
    }
}
