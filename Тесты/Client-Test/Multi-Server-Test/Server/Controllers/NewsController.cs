using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData.Blocks;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Multi_Server_Test.Server.Controllers
{
    public class NewsController : Controller
    {
        public NewsController(string name, List<Model> controllerModel) : base(name, controllerModel) { }

        
        public override void ExecuteRouting(string modelAction, Package package, TcpClient sender)
        {
            var findModel = ExecuteModelAction(modelAction);
            if (findModel != null)
            {
                var clientStream = sender.GetStream();
                findModel.CompleteAction(package.SendingObject, clientStream);
            }
        }
    }
}
