using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.Server.Views;
using Multi_Server_Test.ServerData.Blocks;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Multi_Server_Test.Server.Controllers
{
    public class SourceController : Controller
    {
        public SourceController(string name, List<Model> controllerModel) : base(name, controllerModel) { }

        public override void ExecuteRouting(string requestCommand, Package package, TcpClient sender)
        {
            var responseData = ExecuteModelAction(requestCommand, package);
            var clientStream = sender.GetStream();
            new SourceView(responseData, clientStream).ExecuteModuleProcessing("");
        }
    }
}
