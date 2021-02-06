using Multi_Server_Test.Server.Packages;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Blocks;
using WpfApp1.Server.Packages;

namespace WpfApp1.Classes
{
    public class JumboServer
    {
        //private JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        //{
        //    TypeNameHandling = TypeNameHandling.Auto,
        //    Formatting = Formatting.Indented
        //};

        private TcpClient TCPclient = null;
        public Person ActiveUser = null;
        private ServerConfig ServerConfig = null;

        public JumboServer(ServerConfig config)
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
        public async Task<NewsCollection> ReceiveNewsCollection()
        {
            var meta = new PackageMeta("127.0.0.1", "news");
            var nullNews = new News(); //TODO: ИСПРАВИТЬ КАЛОВЫЙ КОНСТРУКТОР + ВОЗМОЖНОСТЬ ОТПРАВЛЯТЬ ТОЛЬКО МЕТУ НА СЕРВЕР
            var jsonCollection = await SendAndGet(nullNews, meta);
            var collectionResponse =  JsonConvert.DeserializeObject<NewsCollection>(jsonCollection);
            if (collectionResponse == null)
                throw new NullReferenceException("Получена пустая коллекция!");
            else
                return collectionResponse;
        }

        private async Task<string> SendAndGet(RequestObject sendObject, PackageMeta meta)
        {
            await SendRequestAsync(sendObject, meta);
            var jsonResponse = await GetResponseAsync();
            TCPclient.Close();
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
