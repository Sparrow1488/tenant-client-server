using System;
using Newtonsoft.Json;
using WpfApp1.Server.Packages;

namespace WpfApp1.Server.Packages
{
    public class Package<T>
        where T: RequestObject
    {
        public Package(RequestObject sendObj, PackageMeta meta)
        {
            SendingObject = (T)sendObj;
            SendingMeta = meta;
        }
        public Package(PackageMeta meta)
        {
            SendingObject = null;
            SendingMeta = meta;
        }
        public T SendingObject { get; set; }
        public PackageMeta SendingMeta { get; set; }
    }
    
}
