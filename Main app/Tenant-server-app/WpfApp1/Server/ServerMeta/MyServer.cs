using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Blocks;
using WpfApp1.Classes;
using System.Text.RegularExpressions;
using WpfApp1.Server.Packages;

namespace WpfApp1.Classes
{
    public class MyServer
    {
        private JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };
        //TODO: УБРАТЬ ВСЮ СТАТИКУ БЛИН БЛИНСКИЙ
        private TcpClient client = null;
        private ServerConfig ServerConfig = null;

        public MyServer(ServerConfig config)
        {
            ServerConfig = config;
        }
        public void CreateClient()
        {
            client = new TcpClient();
        }
        public async Task SendRequestAsync(RequestObject sendObject)
        {
            await client.ConnectAsync(ServerConfig.Host, ServerConfig.Port);
            NetworkStream stream = client.GetStream();

            var meta = new SendMeta("127.0.0.1", "auth");
            var pack = new Package<RequestObject>(sendObject, meta);
            string jsonPackage = JsonConvert.SerializeObject(pack);
            byte[] data = Encoding.UTF8.GetBytes(jsonPackage);
            await stream.WriteAsync(data, 0, data.Length);
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
                }
            });
            return response.ToString();
        }
    }
}
