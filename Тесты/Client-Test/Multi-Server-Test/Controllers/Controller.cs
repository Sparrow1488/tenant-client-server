using JumboServer.Functions;
using JumboServer.Meta;
using JumboServer.Models;
using JumboServer.Packages;
using JumboServer.Views;
using System.Collections.Generic;
using System.Net.Sockets;

namespace JumboServer.Controllers
{
    public abstract class Controller
    {
        private ServerFunctions serverFunctions = new ServerFunctions();
        private ServerReportsModule reportsModule = new ServerReportsModule();

        public abstract string ControllerName { get; }
        public byte[] ResponseData { get; set; }
        public abstract void Execute(string requestCommand, Package package, TcpClient sender);
        //public byte[] ExecuteModelAction(string modelAction, Package package)
        //{
        //    ResponseData = ServerMeta.Encoding.GetBytes("Не удалось выполнить запрос") ;
        //    for (int j = 0; j < ControllerModels.Count; j++)
        //    {
        //        var model = ControllerModels[j];
        //        if (model.Action == modelAction)
        //        {
        //            if(model.OnlyAdmin)
        //                CompleteIfAdmin(model, package);
        //            else
        //                Complete(model, package);
        //        }
        //    }
        //    return ResponseData;
        //}
        #region PrivateMethods
        private void CompleteIfAdmin(Package package)
        {
            bool senderIsAdmin = serverFunctions.ForAdminChecker(package?.SendingMeta?.UserToken);
            //if (senderIsAdmin)
            //    ResponseData = model.CompleteAction(package.SendingObject);
            //else ResponseData = ServerMeta.Encoding.GetBytes("У Вас недостаточно прав для выполнения этого запроса");
        }
        //private void Complete(Model model, Package package)
        //{
        //    ResponseData = model.CompleteAction(package.SendingObject);
        //}
        #endregion
    }
}
