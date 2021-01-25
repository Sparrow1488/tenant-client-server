using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server
{
    public class Server_90
    {
        public TcpListener server = null;
        public int port = 8090;
        public string host = null;
        public Server_90(string ip)
        {
            host = ip;
            server = new TcpListener(IPAddress.Parse(ip), port);
        }
        public void GetClient()
        {
            while (true)
            {
                Console.WriteLine($"Wait for connecting...");
                TcpClient getClient = server.AcceptTcpClient();
                Console.WriteLine("Client connecting");

                NetworkStream stream = getClient.GetStream();
                byte[] getData = new byte[1024];
                StringBuilder response = new StringBuilder();

                if (stream.CanRead)
                {
                    do
                    {
                        int bytes = stream.Read(getData, 0, getData.Length);
                        response.Append(Encoding.UTF8.GetString(getData, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Get data: " + response);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine("Get stream can not read");
                }

                byte[] sendData = Encoding.UTF8.GetBytes("Ответ сервера");
                stream.Write(sendData, 0, sendData.Length);
                Console.WriteLine("Write data in " + stream.WriteTimeout);

                stream.Close();
            }
        }
    }
}
