using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Server.Packages.LettersDir
{
    public class SendLetterPackage : Package
    {
        public SendLetterPackage(Letter sendObj) : base(sendObj)
        {
            SendingMeta = new PackageMeta(new ServerConfig().HOST, "Letter/send");
        }
    }
}
