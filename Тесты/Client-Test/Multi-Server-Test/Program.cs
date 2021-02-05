using System;
using System.Threading.Tasks;
using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData;

namespace Multi_Server_Test
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            MyServer myServer = new MyServer("127.0.0.1", 8090);
            myServer.Start();
            var list = new NewsList(new News("title1", "desc"), new News("title2", "sdfdf")) ;
            //await myServer.AddNews(list);

            //TODO: сделать отправку сообщений председателю
            //TODO: сделать клиент председателя
            myServer.ReseveAndServeClient();
            Console.WriteLine("Server disconnect.");
        }
    }
    
}

