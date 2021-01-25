using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server_Test
{
    public class ServerPorts
    {
        public TcpListener server = null;

        private string HOST = null;
        private int PORT = 0;
        public string NAME = "ServerName";
        public ServerPorts(string host, int port, string serverName)
        {
            HOST = host;
            PORT = port;
            NAME = serverName;
            server = new TcpListener(IPAddress.Parse(HOST), PORT);
            server.Start();
            Console.WriteLine("Server: {0} started!", NAME);

            Thread thread = new Thread(new ThreadStart(GetClient));
            thread.Start();
        }

        public void GetClient()
        {
            string previewText = $"[{NAME}]";
            try
            {
                while (true)
                {
                    Console.WriteLine("{0}Wait for connecting...", previewText);
                    TcpClient getClient = server.AcceptTcpClient();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("{0}Client connecting", previewText, ConsoleColor.Red);
                    Console.ForegroundColor = ConsoleColor.White;

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
                        Console.WriteLine("{0}Get data: {1}", previewText, response);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine("{0}Get stream can not read", previewText);
                    }

                    string sendString = $"{NAME}:{PORT}: Это полученный Вами ответ сервера!";
                    byte[] sendData = Encoding.UTF8.GetBytes(sendString);
                    stream.Write(sendData, 0, sendData.Length);
                    Console.WriteLine("{0}Data send complete", previewText);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
