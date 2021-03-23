using JumboServer.Functions;
using JumboServer.Meta;
using JumboServer.Models;
using JumboServer.Packages;
using JumboServer.Views;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace JumboServer.Controllers
{
    public abstract class Controller
    {
        private ServerFunctions serverFunctions = new ServerFunctions();
        private ServerReportsModule reportsModule = new ServerReportsModule();

        public string ControllerName { get; }
        public byte[] ResponseData { get; set; }
        protected List<Model> ControllerModels { get; }
        public Controller(string name, List<Model> controllerModel)
        {
            ControllerName = name;
            ControllerModels = controllerModel;
        }
        public abstract void ExecuteRouting(string requestCommand, ref Package package, ref TcpClient sender);
        public byte[] ExecuteModelAction(string modelAction, Package package)
        {
            ResponseData = ServerMeta.Encoding.GetBytes("Не удалось выполнить запрос") ;
            for (int j = 0; j < ControllerModels.Count; j++)
            {
                var model = ControllerModels[j];
                if (model.Action == modelAction)
                {
                    if(model.OnlyAdmin)
                        CompleteIfAdmin(ref model, ref package);
                    else
                        Complete(ref model, ref package);
                }
            }
            return ResponseData;
        }
        public void CompleteDefaultControllerProcessing(string requestCommand, Package package, ViewModule view)
        {
            var responseData = ExecuteModelAction(requestCommand, package);
            view.ExecuteModuleProcessing(ref responseData);
        }

        #region PrivateMethods
        private void CompleteIfAdmin(ref Model model, ref Package package)
        {
            bool senderIsAdmin = serverFunctions.ForAdminChecker(package?.SendingMeta?.UserToken);
            if (senderIsAdmin)
                ResponseData = model.CompleteAction(package.SendingObject);
            else ResponseData = ServerMeta.Encoding.GetBytes("У Вас недостаточно прав для выполнения этого запроса");
        }
        private void Complete(ref Model model, ref Package package)
        {
            ResponseData = model.CompleteAction(package.SendingObject);
        }
        #endregion
    }
}
