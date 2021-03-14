using System;
using System.Net;

namespace WpfApp1.Server.ServerMeta
{
    public class ServerConfig
    {
        public string HOST = "127.0.0.1";
        public int PORT = 8090;
        public IPEndPoint serverEndPoint = null;

        public ServerConfig(string host, int port)
        {
            if(!string.IsNullOrWhiteSpace(host) && port > 0)
            {
                HOST = host;
                PORT = port;
                serverEndPoint = new IPEndPoint(IPAddress.Parse(HOST), PORT);
            }
        }
        public ServerConfig()
        {
            serverEndPoint = new IPEndPoint(IPAddress.Parse(HOST), PORT);
        }
    }
}
