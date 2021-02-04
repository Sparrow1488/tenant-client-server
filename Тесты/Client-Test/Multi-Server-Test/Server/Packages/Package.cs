using System;

namespace Multi_Server_Test.Server.Packages
{
    public class Package
    {
        public Package(RequestObject sendObj, SendMeta meta)
        {
            SendingObject = sendObj;
            SendingMeta = meta;
        }
        public object SendingObject { get; set; }
        public SendMeta SendingMeta { get; set; }
    }
}
