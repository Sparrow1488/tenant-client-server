using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData.Blocks;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Multi_Server_Test.Server.Controllers
{
    public class LettersController : Controller
    {
        public LettersController(string name, List<Model> controllerModel) : base(name, controllerModel) { }

        public override void ExecuteRouting(string requestCommand, Package package, TcpClient sender)
        {
            ExecuteModelAction(requestCommand, package, sender);
        }
    }
}
