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

        public override void ExecuteRouting(string requestCommand, Package package, TcpClient sender)
        {
            var responseData = ExecuteModelAction(requestCommand, package);
            var clientStream = sender.GetStream();
            new UserView(responseData, clientStream).ExecuteModuleProcessing();
        }
    }
}
