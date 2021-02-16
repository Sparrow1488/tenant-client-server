using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChairmanClient.Server.Packages
{
    public class PackageMeta
    {
        public PackageMeta(string address, string action)
        {
            Address = address;
            Action = action;
            FromHostName = Dns.GetHostName();
        }
        public string Address { get; }
        public string Action { get; }
        public string FromHostName { get; }
    }
}
