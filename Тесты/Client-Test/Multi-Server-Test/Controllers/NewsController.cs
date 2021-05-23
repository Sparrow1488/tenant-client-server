using JumboServer.Packages;
using System.Net.Sockets;

namespace JumboServer.Controllers
{
    public class NewsController : Controller
    {
        public override string ControllerName => "News";

        public override void Execute(string requestCommand, Package package, TcpClient sender)
        {

        }
    }
}
