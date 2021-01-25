using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server
{
    class Program
    {
        static int port = 8090;
        static string ip = "127.0.0.1";
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Parse(ip), port);
            server.Start();
            Console.WriteLine("Create");

            
        }
    }
}
