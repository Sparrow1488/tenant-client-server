﻿using JumboServer.Functions;
using System.Net.Sockets;

namespace JumboServer.Views
{
    public class NewsView : ViewModule
    {
        ServerReportsModule serverEvents = new ServerReportsModule();
        public NewsView(ref TcpClient client) : base(ref client) { ViewName = "NewsView"; }

        public override void ExecuteModuleProcessing(ref byte[] responseData)
        {
            SendResponse(ref responseData);
        }
    }
}
