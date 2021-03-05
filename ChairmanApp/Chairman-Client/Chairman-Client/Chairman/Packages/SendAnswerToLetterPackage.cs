using Chairman_Client.Server.Packages.LettersDir;
using WpfApp1.Server.Packages;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client.Chairman.Packages
{
    public class SendAnswerToLetterPackage : Package
    {
        public SendAnswerToLetterPackage(ReplyLetter sendObj) : base(sendObj)
        {
            SendingMeta = new PackageMeta(new ServerConfig().HOST, "Letter/reply");
        }
    }
}
