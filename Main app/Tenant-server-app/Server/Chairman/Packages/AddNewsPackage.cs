using System;
using WpfApp1.Server.Packages;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client.Chairman.Packages
{
    public class AddNewsPackage : Package
    {
        public AddNewsPackage(RequestObject sendObj) : base(sendObj)
        {
            SendingMeta = new PackageMeta(new ServerConfig().HOST, "News/add");
        }
    }
}
