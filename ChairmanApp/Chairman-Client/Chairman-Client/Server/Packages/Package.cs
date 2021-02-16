using System;
using Newtonsoft.Json;

namespace ChairmanClient.Server.Packages
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
        public T SendingObject { get; }
        public PackageMeta SendingMeta { get; }
    }
    
}
