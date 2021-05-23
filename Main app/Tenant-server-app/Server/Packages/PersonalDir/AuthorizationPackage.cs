using System;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Server.Packages.PersonalDir
{
    public class AuthorizationPackage : Package
    {
        public AuthorizationPackage(Person sendObj) : base(sendObj)
        {
            SendingMeta = new PackageMeta(new ServerConfig().HOST, "User/auth");
        }
    }
}
