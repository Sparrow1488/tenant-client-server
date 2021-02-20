using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Server.Packages.NewsDir
{
    public class RecieveNewsPackage : Package
    {
        public RecieveNewsPackage() 
        { 
            SendingMeta = new PackageMeta(new ServerConfig().HOST, "News/get"); 
        }
    }
}
