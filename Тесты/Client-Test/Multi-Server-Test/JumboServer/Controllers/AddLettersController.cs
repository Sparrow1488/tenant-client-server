using JumboServer.Models;
using JumboServer.Packages;
using JumboServer.Views;
using System.Collections.Generic;
using System.Net.Sockets;

namespace JumboServer.Controllers
{
    public class AddLettersController : Controller
    {
        public override string ControllerName => "AddLetters";

        public override void Execute(string requestCommand, Package package, TcpClient sender)
        {

        }
    }
}
