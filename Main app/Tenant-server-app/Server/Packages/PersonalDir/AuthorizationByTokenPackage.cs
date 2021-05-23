using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Server.Packages.PersonalDir
{
    public class AuthorizationByTokenPackage : Package
    {
        public AuthorizationByTokenPackage(UserToken sendObj) : base(sendObj)
        {
            SendingMeta = new PackageMeta(new ServerConfig().HOST, "User/auth-token");
        }
    }
}
