using Multi_Server_Test.Server.Controllers;
using Multi_Server_Test.Server.Packages;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Multi_Server_Test.ServerData.Blocks
{
    public class MainRouter
    {
        private List<Controller> allControllers { get; }
        public MainRouter(List<Controller> existingControllers)
        {
            allControllers = existingControllers;
        }

        public void ExecuteRouting(Package package, TcpClient connectedClient)
        {
            string request = package.SendingMeta.Action;
            string[] fullRequest = request.Split('/');
            string controllerName = fullRequest[0];
            string modelAction = fullRequest[1] != null ? fullRequest[1] : "";

            for (int i = 0; i < allControllers.Count; i++)
            {
                var controller = allControllers[i];
                if (controller.ControllerName == controllerName)
                {
                    controller.ExecuteRouting(modelAction, package, connectedClient);
                    break;
                }
                else if (i == allControllers.Count - 1)
                {
                    Console.WriteLine("MainRouter> Ошибка: Не найдено модели под запрос");
                }
            }
            
        }
    }
}
