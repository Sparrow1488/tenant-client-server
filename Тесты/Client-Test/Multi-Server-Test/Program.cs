using System;
using Multi_Server_Test.ServerData;

namespace Multi_Server_Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            MyServer myServer = new MyServer("127.0.0.1", 8090);
            myServer.Start();

            //TODO: сделать отправку сообщений председателю
            //TODO: сделать клиент председателя
            myServer.ReseveAndServeClient();
        }
    }
    
}

