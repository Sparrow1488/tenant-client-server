using Multi_Server_Test.Server;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Server.Packages;
using WpfApp1.Server.Packages.Letters;

namespace WpfApp1.Server.ServerMeta
{
    public class JumboServer
    {
        
        public static JumboServer ActiveServer;
        private TcpClient TCPclient = null;
        public Person ActiveUser = null;
        private ServerConfig ServerConfig = null;

        public JumboServer(ServerConfig config)
        {
            ServerConfig = config;
            ActiveServer = this;
        }
        public async Task<bool> AuthorizationAsync(Person dataPerson, bool token) //TODO: на сервере: сделать лист с токенами и проверять их при получении от пользователей
        {
            PackageMeta meta = new PackageMeta(ServerConfig.HOST, "auth");

            var jsonResponse = await SendAndGetAsync(dataPerson, meta);
            ActiveUser = JsonConvert.DeserializeObject<Person>(jsonResponse);
            if (ActiveUser.Equals(null))
            {
                throw new Exception("Данный пользователь не существует");
            }
            return true;
        }
        public async Task<NewsCollection> ReceiveNewsCollectionAsync()
        {
            var meta = new PackageMeta("127.0.0.1", "news");
            //var nullNews = new News(); //TODO: ИСПРАВИТЬ КАЛОВЫЙ КОНСТРУКТОР + ВОЗМОЖНОСТЬ ОТПРАВЛЯТЬ ТОЛЬКО МЕТУ НА СЕРВЕР
            var jsonCollection = await SendAndGetAsync(null, meta);
            var collectionResponse =  JsonConvert.DeserializeObject<NewsCollection>(jsonCollection);
            if (collectionResponse == null)
                throw new NullReferenceException("Получена пустая коллекция!");
            else
                return collectionResponse;
        }

        private async Task<string> SendAndGetAsync(RequestObject sendObject, PackageMeta meta)
        {
            await SendRequestAsync(sendObject, meta);
            var jsonResponse = await GetResponseAsync();
            TCPclient.Close();
            return jsonResponse;
        }

        private async Task SendRequestAsync(RequestObject sendObject, PackageMeta meta)
        {
            TCPclient = new TcpClient();
            await TCPclient.ConnectAsync(ServerConfig.HOST, ServerConfig.PORT); //TODO: сделать таймер подключения к серверу (например 10 секунд)
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

        public async Task<string> SendLetter(Letter letter)
        {
            var meta = new PackageMeta(ServerConfig.HOST, "letter");
            return await SendAndGetAsync(letter, meta);
        }
    }
    //private JsonSerializerSettings JsonSettings = new JsonSerializerSettings
    //{
    //    TypeNameHandling = TypeNameHandling.Auto,
    //    Formatting = Formatting.Indented
    //};
}
