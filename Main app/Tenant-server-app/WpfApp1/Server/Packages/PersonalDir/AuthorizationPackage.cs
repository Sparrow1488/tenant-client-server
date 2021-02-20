using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Server.Packages.PersonalDir
{
    public class AuthorizationPackage : Package
    {
        public AuthorizationPackage(RequestObject sendObj) : base(sendObj)
        {
            SendingMeta = new PackageMeta(new ServerConfig().HOST, "User/auth");
        }
    }
}
