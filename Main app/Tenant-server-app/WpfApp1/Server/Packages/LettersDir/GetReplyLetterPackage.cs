using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Server.Packages.LettersDir
{
    public class GetReplyLetterPackage : Package
    {
        public GetReplyLetterPackage(RequestObject sendObj) : base(sendObj)
        {
            SendingMeta = new PackageMeta(new ServerConfig().HOST, "Letter/get-reply");
        }
    }
}
