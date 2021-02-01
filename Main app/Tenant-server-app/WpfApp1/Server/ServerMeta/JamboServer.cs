﻿using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Blocks;
using WpfApp1.Server.Packages;

namespace WpfApp1.Classes
{
    public class JamboServer
    {
        //private JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        //{
        //    TypeNameHandling = TypeNameHandling.Auto,
        //    Formatting = Formatting.Indented
        //};

        private TcpClient TCPclient = null;
        private Person ActiveUser = null;
        private ServerConfig ServerConfig = null;

        public JamboServer(ServerConfig config)
        {
            ServerConfig = config;
        }
        public void ShowUserInfo()
        {
            MessageBox.Show($"Login: {ActiveUser.Login};\n " +
                 $"Password: {ActiveUser.Password};\n " +
                $"Room number: {ActiveUser.Room};\n " +
                $"Name: {ActiveUser.Name}; \n " +
                $"Parent name: {ActiveUser.ParentName};", 
                "User information");
        }
        public bool ActiveUserCheckNull()
        {
            if (ActiveUser.Equals(null))
                return false;
            else
                return true;
        }

        public async Task<bool> Authorization(Person dataPerson)
        {
            PackageMeta meta = new PackageMeta(ServerConfig.HOST, "auth");

            var jsonResponse = await SendAndGet(dataPerson, meta);
            ActiveUser = JsonConvert.DeserializeObject<Person>(jsonResponse);
            if (ActiveUser.Equals(null))
            {
                throw new Exception("Данный пользователь не существует");
            }
            return true;
        }

        public async Task<string> SendAndGet(RequestObject sendObject, PackageMeta meta)
        {
            await SendRequestAsync(sendObject, meta);
            var jsonResponse = await GetResponseAsync();
            return jsonResponse;
        }

        private async Task SendRequestAsync(RequestObject sendObject, PackageMeta meta)
        {
            TCPclient = new TcpClient();
            await TCPclient.ConnectAsync(ServerConfig.HOST, ServerConfig.PORT);
            NetworkStream stream = TCPclient.GetStream();

            var pack = new Package<RequestObject>(sendObject, meta);
            string jsonPackage = JsonConvert.SerializeObject(pack);
            byte[] data = Encoding.UTF8.GetBytes(jsonPackage);
            await stream.WriteAsync(data, 0, data.Length);
        }

        private async Task<string> GetResponseAsync()
        {
            StringBuilder response = new StringBuilder();
            byte[] getData = new byte[2048];
            await Task.Run(() =>
            {
                var serverStream = TCPclient.GetStream();
                if (serverStream.CanRead)
                {
                    do
                    {
                        int bytesSize = serverStream.Read(getData, 0, getData.Length);
                        response.Append(Encoding.UTF8.GetString(getData, 0, bytesSize));
                    }
                    while (serverStream.DataAvailable);
                }
                serverStream.Close();
                TCPclient.Close();
            });
            
            return response.ToString();
        }
    }
}
