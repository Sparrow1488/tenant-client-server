using System;
using System.Net;

namespace Multi_Server_Test.Server.Packages
{
    public class SendMeta
    {
        public SendMeta(string address, string action)
        {
            Address = address;
            Action = action;
            FromHostName = Dns.GetHostName(); //TODO: зачем получаю адресс сервера?
        }
        public string Address { get; }
        public string Action { get; }
        public string FromHostName { get; }
    }
}
