using JumboServer;
using Multi_Server_Test.Server.Models;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using System;

namespace Multi_Server_Test
{
    public class ServerReportsModule
    {
        public void BlockReport(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void BlockReport(Model model, string message, ConsoleColor color)
        {
            Console.Write(model.Action + "> ");

            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void BlockReport(ViewModule module, string message, ConsoleColor color)
        {
            Console.Write(module.ViewName + "> ");

            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void BlockReport(MyServer server, string message, ConsoleColor color)
        {
            Console.Write(server.serverName + "> ");

            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
