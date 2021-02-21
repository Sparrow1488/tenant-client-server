using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData.Blocks;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Multi_Server_Test.Server.Controllers
{
    public abstract class Controller
    {
        public string ControllerName { get; }
        protected List<Model> ControllerModels { get; }
        public Controller(string name, List<Model> controllerModel)
        {
            ControllerName = name;
            ControllerModels = controllerModel;
        }
        public abstract void ExecuteRouting(string requestCommand, Package package, TcpClient sender);
        public byte[] ExecuteModelAction(string modelAction, Package package)
        {
            byte[] modelResponseData = null;
            for (int j = 0; j < ControllerModels.Count; j++)
            {
                var model = ControllerModels[j];
                if (model.Action == modelAction)
                {
                    modelResponseData = model.CompleteAction(package.SendingObject).Result;
                    return modelResponseData;
                }
                else if (j == ControllerModels.Count - 1)
                {
                    Console.WriteLine(ControllerName + "> Не найдено модели под запрос");
                }
            }
            return modelResponseData;
        }
    }
}
