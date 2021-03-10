using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Multi_Server_Test.Server.Functions;
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
            myServer.ServeAndResponseToClient();
            Console.WriteLine("Server disconnect.");
        }
    }
}

