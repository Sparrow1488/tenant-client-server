using WpfApp1.Server.Packages;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client.Chairman.Packages
{
    public class GetLettersPackage : Package
    {
        public GetLettersPackage()
        {
            SendingMeta = new PackageMeta(new ServerConfig().HOST, "Letter/get-all");
        }
    }
}
