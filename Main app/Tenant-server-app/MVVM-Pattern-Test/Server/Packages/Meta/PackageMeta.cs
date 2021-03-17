using System.Net;
using WpfApp1.Server.Packages.PersonalDir;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Server.Packages
{
    public class PackageMeta
    {
        public PackageMeta(string address, string action)
        {
            Address = address;
            Action = action;
            FromHostName = Dns.GetHostName();
            UserToken = JumboServer.ActiveServer?.ActiveUser?.Token;
        }
        public string Address { get; }
        public string Action { get; }
        public string FromHostName { get; }
        public UserToken UserToken { get; }
    }
}
