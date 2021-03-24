using System.Collections.Generic;
using System.Net.Sockets;
using JumboServer.Views;
using JumboServer.Packages;
using JumboServer.Models;

namespace JumboServer.Controllers
{
    public class UserController : Controller
    {
        public UserController(string name, List<Model> controllerModel) : base(name, controllerModel) { }

        public override void ExecuteRouting(string requestCommand, ref Package package, ref TcpClient sender)
        {
            var view = new DefaultAPI_View(ref sender);
            CompleteDefaultControllerProcessing(requestCommand, package, view);
        }
    }
}
