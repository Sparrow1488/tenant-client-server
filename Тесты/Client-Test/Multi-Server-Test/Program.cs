using System;
using System.Threading.Tasks;
using Multi_Server_Test.ServerData;

namespace Multi_Server_Test
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            MyServer myServer = new MyServer("127.0.0.1", 8090);
            await myServer.Start();

            //TODO: сделать отправку сообщений председателю
            //TODO: сделать клиент председателя
            myServer.ReseveAndResponseToClient();
            Console.WriteLine("Server disconnect.");
        }
    }
    
}

