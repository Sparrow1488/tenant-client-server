using JumboServer;
using System;
using JumboServer.Views;
using JumboServer.Models;
using JumboServer.Controllers;

namespace JumboServer.Functions
{
    public class ServerReportsModule
    {
        public void BlockReport(string message, ConsoleColor color)
        {
            WriteOnConsole(message, color);
        }
        public void BlockReport(Model model, string message, ConsoleColor color)
        {
            Console.Write(model.Action + "> ");
            WriteOnConsole(message, color);
        }
        public void BlockReport(ViewModule module, string message, ConsoleColor color)
        {
            Console.Write(module.ViewName + "> ");
            WriteOnConsole(message, color);
        }
        public void BlockReport(MyServer server, string message, ConsoleColor color)
        {
            Console.Write(server.serverName + "> ");
            WriteOnConsole(message, color);
        }
        public void BlockReport(Controller controller, string message, ConsoleColor color)
        {
            Console.Write(controller.ControllerName + "> ");
            WriteOnConsole(message, color);
        }

        public void BlockReport(string speaker, string message, ConsoleColor color)
        {
            Console.Write(speaker + "> ");
            WriteOnConsole(message, color);
        }
        private void WriteOnConsole(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
