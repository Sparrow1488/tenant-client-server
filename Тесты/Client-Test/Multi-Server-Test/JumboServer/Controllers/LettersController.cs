using JumboServer.Models;
using JumboServer.Packages;
using JumboServer.Views;
using System.Collections.Generic;
using System.Net.Sockets;

namespace JumboServer.Controllers
{
    public class LettersController : Controller
    {
        public LettersController(string name, List<Model> controllerModel) : base(name, controllerModel) { }

        public override void ExecuteRouting(string requestCommand, ref Package package, ref TcpClient sender)
        {
            var view = new DefaultAPI_View(ref sender);
            CompleteDefaultControllerProcessing(requestCommand, package, view);
        }
    }
}
