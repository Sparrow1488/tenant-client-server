using System;
using WpfApp1.Classes;
using Newtonsoft.Json;
using WpfApp1.Server.Packages;

namespace WpfApp1.Blocks
{
    public class Package
    {
        public RequestObject SendingObject { get; set; }
        public SendMeta SendingMeta { get; set; }
        public Package(RequestObject request, SendMeta meta)
        {
            SendingObject = request;
            SendingMeta = meta;
        }
    }
    
}
