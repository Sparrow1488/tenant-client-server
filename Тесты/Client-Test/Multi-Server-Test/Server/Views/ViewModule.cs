using Multi_Server_Test.Server.Packages;
using System.Net.Sockets;

namespace Multi_Server_Test.Server.Models
{
    public abstract class ViewModule
    {
        public RequestObject ResponseObject { get; }
        public TcpClient ConnectedClient { get; }
        public ViewModule(RequestObject responseObject, TcpClient client)
        {
            ResponseObject = responseObject;
            ConnectedClient = client;
        }
        public abstract string ExecuteModuleProcessing();
    }
}
