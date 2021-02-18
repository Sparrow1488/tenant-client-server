using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData.Blocks;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Multi_Server_Test.Server.Controllers
{
    public class UserController : Controller
    {
        public UserController(string name, List<Model> controllerModel) : base(name, controllerModel) { }

        public override void ExecuteRouting(string requestCommand, Package package, TcpClient sender)
        {
            var findModel = ExecuteModelAction(requestCommand);
            if (findModel != null)
            {
                var stream = sender.GetStream();
                findModel.CompleteAction(package.SendingObject, stream);
            }
            else
            {
                throw new Exception("Все крайне плохо");
            }
        }
    }
}
