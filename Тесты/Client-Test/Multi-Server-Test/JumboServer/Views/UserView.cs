using JumboServer.Functions;
using System.Net.Sockets;

namespace JumboServer.Views
{
    public class UserView : ViewModule
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        public UserView(ref TcpClient client) : base(ref client) { ViewName = "UserView"; }

        public override void ExecuteModuleProcessing(ref byte[] responseData)
        {
            SendResponse(ref responseData);
        }
    }
}
