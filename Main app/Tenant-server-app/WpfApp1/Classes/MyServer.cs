using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    public class MyServer
    {
        private static TcpClient client = null;
        public static ServerConfig ServerConfig = null;

        public MyServer(ServerConfig config)
        {
            ServerConfig = config;
        }
        public void CreateClient()
        {
            client = new TcpClient();
        }
        public async Task<bool> SendAsync(string sendData)
        {
            await client.ConnectAsync(ServerConfig.Host, ServerConfig.Port);
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(sendData);
            await stream.WriteAsync(data, 0, data.Length);
            //stream.Close();
            return true;
        }
        public async Task<string> GetAsync()
        {
            //await client.ConnectAsync(ServerConfig.Host, ServerConfig.Port);
            StringBuilder response = new StringBuilder();
            byte[] getData = new byte[2048];
            await Task.Run(() =>
            {
                var getStream = client.GetStream();
                if (getStream.CanRead)
                {
                    do
                    {
                        int bytesSize = getStream.Read(getData, 0, getData.Length);
                        response.Append(Encoding.UTF8.GetString(getData, 0, bytesSize));
                    }
                    while (getStream.DataAvailable);
                    //getStream.Close();
                }
            });
            return response.ToString();
        }
    }
}
