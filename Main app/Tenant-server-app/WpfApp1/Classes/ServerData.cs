using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    public class ServerData
    {
        public static string HOST = "127.0.0.1";
        public static int PORT = 8090;
        public static IPAddress IP = IPAddress.Parse(HOST);
        public static IPEndPoint serverEndPoint = new IPEndPoint(IP, PORT);
    }
}
