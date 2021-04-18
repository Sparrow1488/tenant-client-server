using System;
using Newtonsoft.Json;
using WpfApp1.Server.Packages;

namespace WpfApp1.Server.Packages
{
    public abstract class Package
    {
        protected Package(RequestObject sendObj)
        {
            SendingObject = sendObj;
        }
        protected Package() { }
        protected Package(string request) 
        {
            SendingObject = request;
        }

        public object SendingObject { get; }
        public PackageMeta SendingMeta { get; protected set; }
    }
    
}
