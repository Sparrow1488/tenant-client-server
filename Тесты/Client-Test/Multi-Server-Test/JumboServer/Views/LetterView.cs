using JumboServer.Functions;
using System.Net.Sockets;

namespace JumboServer.Views
{
    public class LetterView : ViewModule
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        public LetterView(ref TcpClient client) : base(ref client) { ViewName = "LetterView"; }

        public override void ExecuteModuleProcessing(ref byte[] responseData)
        {
            SendResponse(ref responseData);
        }
    }
}
