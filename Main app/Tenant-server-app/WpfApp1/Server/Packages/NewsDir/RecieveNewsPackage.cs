using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Server.Packages.NewsDir
{
    public class RecieveNewsPackage : Package
    {
        public RecieveNewsPackage() { SendingMeta = new PackageMeta("ytdf;yj", "News/get"); }
    }
}
