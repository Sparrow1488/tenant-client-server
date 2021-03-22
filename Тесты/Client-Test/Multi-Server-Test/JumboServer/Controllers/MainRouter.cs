using JumboServer.Packages;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace JumboServer.Controllers
{
    public class MainRouter
    {
        private List<Controller> AllControllers { get; }
        public MainRouter(List<Controller> projectControllers)
        {
            if (projectControllers == null || projectControllers.Count == 0)
                throw new ArgumentNullException("Вы не можете создать проект, не задействова ни одного контроллера!");
            AllControllers = projectControllers;
        }

        private string controllerName = string.Empty;
        private string modelAction = string.Empty;
        private string requestAction = string.Empty;
        public void ExecuteRouting(Package package, TcpClient connectedClient)
        {
            requestAction = package?.SendingMeta?.Action;
            string[] requestComponents = requestAction.Split('/');
            if (requestComponents.Length < 2) return;
            controllerName = requestComponents[0] ?? "info";
            modelAction = requestComponents[1] ?? "info";

            for (int i = 0; i < AllControllers.Count; i++)
            {
                var controller = AllControllers[i];
                if (controller.ControllerName == controllerName)
                {
                    controller.ExecuteRouting(modelAction, package, connectedClient);
                    break;
                }

                if (i == AllControllers.Count - 1)
                    Console.WriteLine("MainRouter> Ошибка: Не найдено модели под запрос");
            }
        }
    }
}
