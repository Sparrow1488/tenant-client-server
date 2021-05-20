using JumboServer.Functions;
using JumboServer.Packages;
using Multi_Server_Test.JumboServer.Controllers;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace JumboServer.Controllers
{
    public class Router
    {

        private ServerReportsModule reportsModule = new ServerReportsModule();
        private string controllerName = string.Empty;
        private string modelAction      = string.Empty;
        private string requestAction    = string.Empty;
        public void ExecuteRouting(Package package, TcpClient connectedClient)
        {
            UnboxingRequest(package);

            ControllerSelector controllerSelector = new ControllerSelector();
            var controller = controllerSelector.SelectOrDefault(controllerName);
            controller?.Execute(requestAction, package, connectedClient);
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
