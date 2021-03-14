using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Server.Packages.SourceDir
{
    public class AddNewsSourcePackage : Package
    {
        public AddNewsSourcePackage(RequestObject sendObj) : base(sendObj)
        {
            SendingMeta = new PackageMeta(new ServerConfig().HOST, "Source/add");
        }
    }
}
