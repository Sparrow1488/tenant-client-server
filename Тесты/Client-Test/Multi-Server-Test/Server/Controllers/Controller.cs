using Multi_Server_Test.Server.Functions;
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
        private ServerFunctions serverFunctions = new ServerFunctions();

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
            bool senderIsAdmin = serverFunctions.ForAdminChecker(package?.SendingMeta?.UserToken);
            for (int j = 0; j < ControllerModels.Count; j++)
            {
                var model = ControllerModels[j];
                if (model.Action == modelAction)
                {
                    if(model.OnlyAdmin)
                    {
                        if(model.OnlyAdmin == senderIsAdmin)
                        {
                            modelResponseData = model.CompleteAction(package.SendingObject);
                            return modelResponseData;
                        }
                    }
                    else
                    {
                        modelResponseData = model.CompleteAction(package.SendingObject);
                        return modelResponseData;
                    }
                }
                else if (j == ControllerModels.Count - 1)
                {
                    Console.WriteLine(ControllerName + "> Не найдено модели под запрос");
                }
            }
            return modelResponseData = Encoding.UTF8.GetBytes("Ошибка запроса: не существует модели под данный запрос!");
        }
    }
}
