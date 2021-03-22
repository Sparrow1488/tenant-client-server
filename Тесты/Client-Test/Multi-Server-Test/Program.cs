using System;
using System.IO;
using JumboServer;
using JumboServer.Functions;

namespace JumboServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fun = new ServerFunctions();
            Console.ForegroundColor = ConsoleColor.White;
            MyServer myServer = new MyServer("127.0.0.1", 8090);
            myServer.Start();

            //var base64 = Convert.ToBase64String(File.ReadAllBytes(@"C:\Users\DOM\Desktop\ИЛЬЯ\HTML\C#\tenant-client-server\Тесты\image3.jpg"));
            //var source = new Source(1, base64, 1, DateTime.Now);
            //var succ = fun.InsertImageInDB(source);
            //Console.WriteLine(succ);
            myServer.ServeAndResponseToClient();
            Console.WriteLine("Server disconnect.");
        }
    }
}

