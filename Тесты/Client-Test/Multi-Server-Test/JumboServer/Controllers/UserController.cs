using System.Collections.Generic;
using System.Net.Sockets;
using JumboServer.Views;
using JumboServer.Packages;
using JumboServer.Models;

namespace JumboServer.Controllers
{
    public class UserController : Controller
    {
        public override string ControllerName => "User";

        public override void Execute(string requestCommand, Package package, TcpClient sender)
        {
        }
    }
}
