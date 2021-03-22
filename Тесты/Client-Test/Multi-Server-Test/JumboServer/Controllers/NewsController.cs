using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using JumboServer.Views;
using JumboServer.Packages;
using JumboServer.Models;

namespace JumboServer.Controllers
{
    public class NewsController : Controller
    {
        public NewsController(string name, List<Model> controllerModel) : base(name, controllerModel) { }
        public override void ExecuteRouting(string requestCommand, Package package, TcpClient sender)
        {
            var reponseData = ExecuteModelAction(requestCommand, package);
            var clientStream = sender.GetStream();
            new NewsView(reponseData, clientStream).ExecuteModuleProcessing();
        }
    }
}
