using System.Net.Sockets;

namespace JumboServer.Views
{
    public class SourceView : ViewModule
    {
        public SourceView(ref TcpClient client) : base(ref client) { ViewName = "SourceView"; }

        public override void ExecuteModuleProcessing(ref byte[] responseData)
        {
            SendResponse(ref responseData);
        }
    }
}
