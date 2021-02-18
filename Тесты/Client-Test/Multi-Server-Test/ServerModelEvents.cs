using Multi_Server_Test.Server.Models;
using Multi_Server_Test.ServerData.Blocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test
{
    public class ServerModelEvents
    {
        public ServerModelEvents()
        {

        }
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
            Console.Write(module + "> ");

            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
