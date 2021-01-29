using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Packages
{
    public class SendMeta
    {
        public SendMeta(string address, string action)
        {
            Address = address;
            Action = action;
        }
        public string Address { get; set; }
        public string Action { get; set; }
    }
}
