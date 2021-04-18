using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Server.Packages.SourceDir
{
    public class GetSourceByTokenPackage : Package
    {
        public GetSourceByTokenPackage(string token) : base(token)
        {
            SendingMeta = new PackageMeta(new ServerConfig().HOST, "Source/get-token");
        }
    }
}
