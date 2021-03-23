using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using JumboServer.Views;
using JumboServer.Packages;
using JumboServer.Models;

namespace JumboServer.Controllers
{
    public class LettersController : Controller
    {
        public LettersController(string name, List<Model> controllerModel) : base(name, controllerModel) { }

        public override void ExecuteRouting(string requestCommand, ref Package package, ref TcpClient sender)
        {
            var view = new LetterView(ref sender);
            CompleteDefaultControllerProcessing(requestCommand, package, view);
        }
    }
}
