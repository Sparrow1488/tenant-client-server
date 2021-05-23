using Multi_Server_Test.Server;
using WpfApp1.Server.Packages;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client.Server.Packages.NewsDir
{
    public class AddNewsPackage : Package
    {
        public AddNewsPackage(News sendObj) : base(sendObj)
        {
            SendingMeta = new PackageMeta(new ServerConfig().HOST, "News/add");
        }
    }
}
