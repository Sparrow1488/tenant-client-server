using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Packages
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
