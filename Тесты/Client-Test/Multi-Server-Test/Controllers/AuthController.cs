using JumboServer.Controllers;
using JumboServer.Packages;
using System.Net.Sockets;
using System;

namespace Multi_Server_Test.JumboServer.Controllers
{
    public class AuthController : Controller
    {
        public override string ControllerName => "Auth";

        public override void Execute(string requestCommand, Package package, TcpClient sender)
        {
            Console.WriteLine("Test");
        }
    }
}
