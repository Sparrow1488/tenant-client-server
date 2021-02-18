using Multi_Server_Test.Server.Packages;
using System.Net.Sockets;

namespace Multi_Server_Test.Server.Models
{
    public class NewsView : ViewModule
    {
        ServerModelEvents serverEvents = new ServerModelEvents();
        public NewsView(RequestObject responseObject, TcpClient client) : base(responseObject, client) { }

        public override string ExecuteModuleProcessing()
        {
            return null;
            //serverEvents.BlockReport(this, "Запрос в GetNewsView");
        }
    }
}
