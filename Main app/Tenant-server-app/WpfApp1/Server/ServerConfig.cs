using System;
using System.Net;

namespace WpfApp1.Classes
{
    public class ServerConfig
    {

        public const string HOST = "127.0.0.1";
        public const int PORT = 8090;
        public static IPAddress IP = IPAddress.Parse(HOST);
        public static IPEndPoint serverEndPoint = new IPEndPoint(IP, PORT);

        public string Host;
        public int Port;
        public ServerConfig(string host, int port)
        {
            if(!string.IsNullOrWhiteSpace(host) && port > 0)
            {
                Host = host;
                Port = port;
            }
        }
    }
}
