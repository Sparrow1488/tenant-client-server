using Multi_Server_Test.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Server.Packages;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.Packages.LettersDir;
using WpfApp1.Server.Packages.NewsDir;
using WpfApp1.Server.Packages.PersonalDir;
using WpfApp1.Server.ServerExceptions;
using System.Linq;

namespace WpfApp1.Server.ServerMeta
{
    public class JumboServer
    {
        public static JumboServer ActiveServer;
        private TcpClient TCPclient = null; //TODO: убрать лишнее
        public Person ActiveUser = null;
        private ServerConfig ServerConfig = null;
        public string tokenFileName = "token-auth";

        public JumboServer(ServerConfig config)
        {
            ServerConfig = config;
            ActiveServer = this;
        }
        public async Task<bool> AuthorizationAsync(Person dataPerson, bool saveToken) //TODO: на сервере: сделать лист с токенами и проверять их при получении от пользователей
        {
            var authPack = new AuthorizationPackage(dataPerson);
            var jsonResponse = await SendAndGetAsync(authPack);

            try { ActiveUser = JsonConvert.DeserializeObject<Person>(jsonResponse); }
            catch(JsonReaderException) { }
            if (ActiveUser == null)
                throw new UserNotExist("Данный пользователь не существует. Возможно, вы ввели не верный логин или пароль");
            if (saveToken && ActiveUser.Token != null)
            {
                using (var sw = File.CreateText(tokenFileName + ".txt"))
                {
                    var userToken = ActiveUser.Token;
                    var jsonToken = JsonConvert.SerializeObject(userToken);
                    sw.WriteLine(jsonToken);
                }
            }
            return true;
        }
        public async Task<bool> AuthorizationByTokenAsync(UserToken token)
        {
            var authPack = new AuthorizationByTokenPackage(token);
            var jsonResponse = await SendAndGetAsync(authPack);

            try { ActiveUser = JsonConvert.DeserializeObject<Person>(jsonResponse); }
            catch (JsonReaderException) { }
            if (ActiveUser == null)
                throw new UserNotExist("Данный пользователь не существует. Возможно, отправленный Вами токен не действителен");
            return true;
        }
        public async Task<List<News>> ReceiveNewsCollectionAsync()
        {
            var pack = new RecieveNewsPackage();
            string jsonCollection = await SendAndGetAsync(pack);
            var collectionResponse = JsonConvert.DeserializeObject<List<News>>(jsonCollection);
            if (collectionResponse == null)
                throw new NullReferenceException("Получена пустая коллекция!");
            var toOrderbyDateCreateNews = from news in collectionResponse
                                          orderby news.DateTime
                                          select news;
            List<News> responseNewsCollection = new List<News>();
            foreach (var news in toOrderbyDateCreateNews)
                responseNewsCollection.Add(news);
            return collectionResponse;
        }

        public async Task<string> SendAndGetAsync(Package package)
        {
            string jsonResponse = null;
            try
            {
                var canSendRequest = await TrySendRequestAsync(package);
                if (canSendRequest)
                {
                    jsonResponse = await GetResponseAsync();
                    TCPclient.Close();
                }
            }
            catch (InvalidOperationException) { }
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
                try{ await TCPclient.ConnectAsync(ServerConfig.HOST, ServerConfig.PORT); }
                catch (SocketException) { }

                connectCounter++;
                if (connectCounter == 3)
                    return false;
            }
            while (TCPclient.Connected != true);
            return true;
        }
        public UserToken DeserializeTokenByFileName(string tokenName)
        {
            UserToken token = null;
            if (File.Exists(tokenName + ".txt")) 
            {
                try
                {
                    using (var sr = File.OpenText(tokenName + ".txt"))
                    {
                        string jsonToken = sr.ReadToEnd();
                        token = JsonConvert.DeserializeObject<UserToken>(jsonToken);
                    }
                }
                catch (Exception) { }
            }
            return token;
        }

        private async Task<string> GetResponseAsync()
        {
            StringBuilder response = new StringBuilder();
            byte[] getData = new byte[2048];
            bool breakConnection = false;
            try
            {
                await Task.Run(() =>
                {
                    if (!TCPclient.Connected || TCPclient == null)
                        throw new ConnectionException("Ошибка подключения к серверу");

                    var serverStream = TCPclient.GetStream();
                    ReadStreamData(serverStream, ref getData, ref response);
                    serverStream.Close();
                    TCPclient.Close();
                });
            }
            catch (IOException) { breakConnection = true; }
            if (breakConnection)
                throw new GetResponseException("Удаленный хост принудительно разорвал существующее подключение");
            return response.ToString();
        }
        private void ReadStreamData(NetworkStream readStream, ref byte[] buffer, ref StringBuilder builder)
        {
            if (readStream.CanRead)
            {
                do
                {
                    int bytesSize = readStream.Read(buffer, 0, buffer.Length);
                    builder.Append(Encoding.UTF8.GetString(buffer, 0, bytesSize));
                }
                while (readStream.DataAvailable);
            }
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
