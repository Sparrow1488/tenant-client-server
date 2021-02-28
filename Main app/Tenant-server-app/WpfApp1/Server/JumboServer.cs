using Multi_Server_Test.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Server.Packages;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.Packages.LettersDir;
using WpfApp1.Server.Packages.NewsDir;
using WpfApp1.Server.Packages.PersonalDir;
using WpfApp1.Server.ServerExceptions;

namespace WpfApp1.Server.ServerMeta
{
    public class JumboServer
    {
        public static JumboServer ActiveServer;
        private TcpClient TCPclient = null; //TODO: убрать лишнее
        public Person ActiveUser = null;
        private ServerConfig ServerConfig = null;

        public JumboServer(ServerConfig config)
        {
            ServerConfig = config;
            ActiveServer = this;
        }
        public async Task<bool> AuthorizationAsync(Person dataPerson, bool saveToken) //TODO: на сервере: сделать лист с токенами и проверять их при получении от пользователей
        {
            var authPack = new AuthorizationPackage(dataPerson);
            var jsonResponse = await SendAndGetAsync(authPack);
            ActiveUser = JsonConvert.DeserializeObject<Person>(jsonResponse);
            if (ActiveUser == null)
                throw new NullReferenceException("Данный пользователь не существует");
            return true;
        }
        public async Task<List<News>> ReceiveNewsCollectionAsync()
        {
            var pack = new RecieveNewsPackage();
            string jsonCollection = await SendAndGetAsync(pack);
            var collectionResponse = JsonConvert.DeserializeObject<List<News>>(jsonCollection);
            if (collectionResponse == null)
                throw new NullReferenceException("Получена пустая коллекция!");
            else
                return collectionResponse;
        }

        public async Task<string> SendAndGetAsync(Package package)
        {
            string jsonResponse = null;
            var canSendRequest = await TrySendRequestAsync(package);
            if (canSendRequest)
            {
                jsonResponse = await GetResponseAsync();
                TCPclient.Close();
            }
            return jsonResponse;
        }

        private async Task<bool> TrySendRequestAsync(Package package)
        {
            TCPclient = new TcpClient();
            if (!TCPclient.Connected)
            {
                TCPclient = new TcpClient();
                var isConnect = await TryConnect();
                if (isConnect)
                {
                    NetworkStream stream = TCPclient.GetStream();
                    string jsonPackage = JsonConvert.SerializeObject(package);
                    byte[] data = Encoding.UTF8.GetBytes(jsonPackage);
                    await stream.WriteAsync(data, 0, data.Length);
                }
                else throw new ConnectionException("Ошибка подключения к серверу");
            }
            return true;
        }
        private async Task<bool> TryConnect()
        {
            int connectCounter = 0;
            do
            {
                try{
                    await TCPclient.ConnectAsync(ServerConfig.HOST, ServerConfig.PORT); //TODO: сделать таймер подключения к серверу (например 10 секунд)
                }
                catch (SocketException) { }

                connectCounter++;
                if (connectCounter == 3)
                    return false;
            }
            while (TCPclient.Connected != true);
            return true;
        }

        private async Task<string> GetResponseAsync()
        {
            StringBuilder response = new StringBuilder();
            byte[] getData = new byte[2048];
            await Task.Run(() =>
            {
                if (!TCPclient.Connected || TCPclient == null)
                    throw new ConnectionException("Ошибка подключения к серверу");

                var serverStream = TCPclient?.GetStream();
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
            var pack = new SendLetterPackage(letter);
            return await SendAndGetAsync(pack);
        }

    }
    //private JsonSerializerSettings JsonSettings = new JsonSerializerSettings
    //{
    //    TypeNameHandling = TypeNameHandling.Auto,
    //    Formatting = Formatting.Indented
    //};
}
