using System;
using System.Threading.Tasks;

namespace JumboServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            MyServer myServer = new MyServer("127.0.0.1", 8090);
            await myServer.StartAsync();
            myServer.ProcessingClients();
            Console.WriteLine("Server disconnect.");
        }
    }
}

