using System;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using System.Net;
using System.Net.Sockets;

namespace Tenant_server_app.ServerClasses
{
    public abstract class ServerData
    {
        public static IPAddress IP = IPAddress.Parse("127.0.0.1");
        public static int PORT = 8080;
        public static TcpListener server = new TcpListener(IP, PORT);

        public static string usersPath = "Users";
        public static FirebaseClient serverClient = null;
        public static IFirebaseConfig firebaseConfig = new FirebaseConfig()
        {
            AuthSecret = "6CScUkKUdSLgSDtq1QWtfY2NCPP57aa6ajBn7R4Y",
            BasePath = "https://client-server-testapp-default-rtdb.firebaseio.com/"
        };
    }
}
