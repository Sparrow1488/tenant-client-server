using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    public class SendMeta
    {
        public SendMeta(string address, string action)
        {
            Address = address;
            Action = action;
        }
        public string Address { get; }
        public string Action { get; }
    }
}
