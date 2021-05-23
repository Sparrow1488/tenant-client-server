using JumboServer.Functions;
using JumboServer.Meta;
using JumboServer.Packages;
using Multi_Server_Test.JumboServer.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

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
        public string ReceiveRequest(TcpClient client)
        {
            var stream = client.GetStream();
            var jsonPackage = GetDataFromStream(stream);
            var getPackage = JsonConvert.DeserializeObject<Package>(serializeDataPackage.ToString());
            Console.WriteLine(getPackage.SendingMeta);

            modulEvents.BlockReport(this, "Distribute request to handle in MainRouter controller...", ConsoleColor.Yellow);
            return jsonPackage;
        }

        #region AdditionalMethods
        private void UnboxingRequest(Package package)
        {
            requestAction = package?.SendingMeta?.Action;
            string[] requestComponents = requestAction.Split('/');
            controllerName = requestComponents[0] ?? "info";
            modelAction = requestComponents[1] ?? "info";
        }
        private string GetDataFromStream(NetworkStream clientStream)
        {
            StringBuilder recievedData = new StringBuilder();
            if (clientStream.CanRead)
            {
                byte[] buffer = new byte[1024];
                recievedData = ReadStreamData(ref buffer, ref clientStream);
            }
            return recievedData.ToString();
        }
        private StringBuilder ReadStreamData(ref byte[] buffer, ref NetworkStream clientStream)
        {
            StringBuilder jsonPackage = new StringBuilder();
            do
            {
                try
                {
                    int bytes = clientStream.Read(buffer, 0, buffer.Length);
                    jsonPackage.Append(ServerMeta.Encoding.GetString(buffer, 0, bytes));
                }
                catch (IOException) { }
            }
            while (clientStream.DataAvailable);
            return jsonPackage;
        }
        #endregion


    }
}
