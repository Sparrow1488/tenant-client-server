using JumboServer.Controllers;
using JumboServer.Packages;
using System.Net.Sockets;

namespace Multi_Server_Test.JumboServer.Controllers
{
    public class AuthController : Controller
    {
        public override string ControllerName => "Auth";

        public override void Execute(string requestCommand, Package package, TcpClient sender)
        {
            System.Console.WriteLine("Test");
        }
    }
}
