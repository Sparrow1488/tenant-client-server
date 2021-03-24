using System.Net.Sockets;

namespace JumboServer.Views
{
    public class DefaultAPI_View : ViewModule
    {
        public DefaultAPI_View(ref TcpClient client) : base(ref client) { ViewName = "ServerAPI_View"; }
        public override void ExecuteModuleProcessing(ref byte[] responseData)
        {
            SendResponse(ref responseData);
        }
    }
}
