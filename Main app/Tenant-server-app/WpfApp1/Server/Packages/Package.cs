using System;
using WpfApp1.Classes;
using Newtonsoft.Json;
using WpfApp1.Server.Packages;

namespace WpfApp1.Blocks
{
    public class Package<T>
        where T: RequestObject
    {
        public Package(RequestObject sendObj, SendMeta meta)
        {
            SendingObject = (T)sendObj;
            SendingMeta = meta;
        }
        public T SendingObject { get; set; }
        public SendMeta SendingMeta { get; set; }
    }
    
}
