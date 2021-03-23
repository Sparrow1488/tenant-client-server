using JumboServer.Functions;
using JumboServer.Packages;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace JumboServer.Controllers
{
    public class MainRouter
    {
        private List<Controller> AllControllers { get; }

        private ServerReportsModule reportsModule = new ServerReportsModule();
        private string controllerName = string.Empty;
        private string modelAction      = string.Empty;
        private string requestAction    = string.Empty;
        public MainRouter(List<Controller> projectControllers)
        {
            if (projectControllers == null || projectControllers.Count == 0)
                throw new ArgumentNullException("Вы не можете создать проект, не используя ни одного контроллера!");
            AllControllers = projectControllers;
        }
        
        public void ExecuteRouting(Package package, TcpClient connectedClient)
        {
            UnboxingRequest(package);

            for (int i = 0; i < AllControllers.Count; i++)
            {
                var controller = AllControllers[i];
                if (controller.ControllerName == controllerName)
                {
                    controller.ExecuteRouting(modelAction, ref package, ref connectedClient);
                    break;
                }
                if (i == AllControllers.Count - 1)
                    reportsModule.BlockReport("MainRouter", "Ошибка: Не найдено модели под запрос", ConsoleColor.Yellow);
            }
        }

        #region AdditionalMethods
        private void UnboxingRequest(Package package)
        {
            requestAction = package?.SendingMeta?.Action;
            string[] requestComponents = requestAction.Split('/');
            controllerName = requestComponents[0] ?? "info";
            modelAction = requestComponents[1] ?? "info";
        }
        #endregion


    }
}
