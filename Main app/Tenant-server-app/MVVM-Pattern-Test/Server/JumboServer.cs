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
using Chairman_Client.Server.Packages.LettersDir;
using WpfApp1.Server.Packages.SourceDir;
using System.Security.Cryptography;
using AesEncryptor;
using RSAEncrypter;
using MVVM_Pattern_Test.Server.Packages;

namespace WpfApp1.Server.ServerMeta
{
    public class JumboServer
    {
        public static JumboServer ActiveServer;
        public Person ActiveUser = null;
        private ServerConfig ServerConfig = null;
        public string tokenFileName = "token-auth";
        private Encoding ServerEncoding = Encoding.UTF32;
        public string PublicRSAServerKey = "ПОЛУЧИ ВАШИСТ ГРАНАТУ!!!!";
        public string AESServerKey = "ПОЛУЧИ ВАШИСТ ГРАНАТУ!!!!";

        public string PublicKey { get; private set; }
        public string PrivateKey { get; private set; }

        public JumboServer(ServerConfig config)
        {
            ServerConfig = config;
            ActiveServer = this;
            PublicRSAServerKey = File.ReadAllText("publicKey.txt");
        }
        public async Task<bool> Authorization(Person dataPerson, bool saveToken)
        {
            var sourceData = File.ReadAllBytes(@"C:\Users\Dom\Desktop\план.rtf");
            var source = new Source(Convert.ToBase64String(sourceData), 1, ".rtf");
            var packSnusa = new AddNewSourcePackage(source);
            byte[] key = MyAes.GenerateKey();
            byte[] IV = MyAes.GenerateIV();
            string jsonPackage = JsonConvert.SerializeObject(packSnusa);
            byte[] encyptData = MyAes.Encrypt(jsonPackage, key, IV);

            RSACryptoServiceProvider rcsp = new RSACryptoServiceProvider(1024);
            RSAParameters privateRsaParam = rcsp.ExportParameters(true);
            RSAParameters publicRsaParam = rcsp.ExportParameters(false);

            string xmlPrivateRsa = MyRSA.RsaToString(privateRsaParam);
            string xmlPublicRsa = MyRSA.RsaToString(publicRsaParam);
            byte[] encryptAesKey = MyRSA.EncryptData(key, MyRSA.StringToRsa(xmlPublicRsa));
            byte[] encryptAesIV = MyRSA.EncryptData(IV, MyRSA.StringToRsa(xmlPublicRsa));
            SecretPackage secPack = new SecretPackage();
            secPack.EncyptPackage = encyptData;
            secPack.KEY = encryptAesKey;
            secPack.IV = encryptAesIV;
            var sendData = JsonConvert.SerializeObject(secPack);

            byte[] receivedAesKey = new byte[256];
            TcpClient client = new TcpClient("че", 888);
            //отправляю серверу байтики
            var writeStream = client.GetStream();
            writeStream.Write(encryptAesKey, 0, encryptAesKey.Length);

            // *сервер их получает*

            writeStream = client.GetStream();
            writeStream.Write(encryptAesIV, 0, encryptAesIV.Length);

            // *сервер их получает*

            writeStream = client.GetStream();
            writeStream.Write(encyptData, 0, encyptData.Length);

            // *сервер их получает*
            // *сервер отправляет ответ*


            //Send DATA

            var receivedData = JsonConvert.DeserializeObject<SecretPackage>(sendData);
            var decryptAesKey = MyRSA.DecryptData(receivedData.KEY, MyRSA.StringToRsa(xmlPrivateRsa));
            var decryptAesIV = MyRSA.DecryptData(receivedData.IV, MyRSA.StringToRsa(xmlPrivateRsa));
            string decryptPackageJson = MyAes.Decrypt(receivedData.EncyptPackage, decryptAesKey, decryptAesIV);
            Package ReceivedPackage = JsonConvert.DeserializeObject<Package>(decryptPackageJson);
            Console.WriteLine(ReceivedPackage);

            //var authPack = new AuthorizationPackage(dataPerson);
            //var jsonResponse = await SendAndGetAsync(authPack);

            //try 
            //{
            //    ActiveUser = null;
            //    ActiveUser = JsonConvert.DeserializeObject<Person>(jsonResponse); 
            //}
            //catch(JsonReaderException) { }
            //if (ActiveUser == null)
            //    return false;
            //if (saveToken && ActiveUser.Token != null)
            //{
            //    using (var sw = File.CreateText(tokenFileName + ".txt"))
            //    {
            //        var userToken = ActiveUser.Token;
            //        var jsonToken = JsonConvert.SerializeObject(userToken);
            //        sw.WriteLine(jsonToken);
            //    }
            //}
            return true;
        }
        public async Task<bool> AuthorizationByTokenAsync(UserToken token)
        {
            var authPack = new AuthorizationByTokenPackage(token);
            var jsonResponse = await SendAndGetAsync(authPack);

            try { ActiveUser = JsonConvert.DeserializeObject<Person>(jsonResponse); }
            catch (JsonReaderException) { }
            if (ActiveUser == null)
                return false;
            return true;
        }
        public async Task<List<News>> ReceiveNewsCollectionAsync()
        {
            List<News> responseNewsCollection = new List<News>();
            var pack = new RecieveNewsPackage();
            string jsonCollection = await SendAndGetAsync(pack);
            var collectionResponse = JsonConvert.DeserializeObject<List<News>>(jsonCollection);
            if (collectionResponse == null)
                return responseNewsCollection;
            var toOrderbyDateCreateNews = from news in collectionResponse
                                          orderby news.DateTime
                                          select news;
            
            foreach (var news in toOrderbyDateCreateNews)
                responseNewsCollection.Add(news);
            return collectionResponse;
        }

        public async Task<string> SendAndGetAsync(Package package)
        {
            string jsonResponse = null;
            try
            {
                TcpClient client = new TcpClient(ServerConfig.HOST, ServerConfig.PORT);
                var canSendRequest = await TrySendRequestAsync(package, client);
                if (canSendRequest)
                {
                    jsonResponse = await GetResponseAsync(client);
                }
            }
            catch (InvalidOperationException) { }
            return jsonResponse;
        }

        private async Task<bool> TrySendRequestAsync(Package package, TcpClient client)
        {
            bool isConnect = client.Connected;
            if (!client.Connected)
                isConnect = await TryConnect(client);
            if (isConnect)
                {
                    NetworkStream stream = client.GetStream();
                    string jsonPackage = JsonConvert.SerializeObject(package);
                    byte[] data = ServerEncoding.GetBytes(jsonPackage);
                    await stream.WriteAsync(data, 0, data.Length);
                }
                else throw new ConnectionException("Ошибка подключения к серверу");
            return true;
        }
        private async Task<bool> TryConnect(TcpClient client)
        {
            int connectCounter = 0;
            if (client.Connected)
                return true;
            do
            {
                try{ await client.ConnectAsync(ServerConfig.HOST, ServerConfig.PORT); }
                catch (SocketException) { }

                connectCounter++;
                if (connectCounter == 3)
                    return false;
            }
            while (client.Connected != true);
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
                catch (Exception) { return null; }
            }
            return token;
        }

        private async Task<string> GetResponseAsync(TcpClient client)
        {
            StringBuilder response = new StringBuilder();
            byte[] getData = new byte[2048];
            bool breakConnection = false;
            try
            {
                await Task.Run(() =>
                {
                    if (!client.Connected || client == null)
                        throw new ConnectionException("Ошибка подключения к серверу");

                    var serverStream = client.GetStream();
                    ReadStreamData(serverStream, ref getData, ref response);
                    serverStream.Close();
                    client.Close();
                });
            }
            catch (IOException) { breakConnection = true; }
            catch  { }
            if (breakConnection)
                throw new GetResponseException("Удаленный хост принудительно разорвал существующее подключение");
            return response.ToString();
        }
        private void ReadStreamData(NetworkStream readStream, ref byte[] buffer, ref StringBuilder builder)
        {
            try
            {
                if (readStream.CanRead)
                {
                    do
                    {
                        int bytesSize = readStream.Read(buffer, 0, buffer.Length);
                        builder.Append(ServerEncoding.GetString(buffer, 0, bytesSize));
                    }
                    while (readStream.DataAvailable);
                }
            }
            catch (IOException) { }
        }

        public async Task<string> SendLetter(Letter letter)
        {
            var pack = new SendLetterPackage(letter);
            return await SendAndGetAsync(pack);
        }

        public async Task<List<ReplyLetter>> GetReplyByLetterId(int id)
        {
            List<ReplyLetter> replyes = new List<ReplyLetter>();
            try
            {
                var pack = new GetReplyLetterPackage(new Letter(id));
                var replyJson = await ActiveServer.SendAndGetAsync(pack);
                replyes = JsonConvert.DeserializeObject<List<ReplyLetter>>(replyJson);
            }
            catch { }
            return replyes;
        }

        public async Task<List<Letter>> GetMyLetters()
        {
            try
            {
                var pack = new GetMyLettersPackage(ActiveServer.ActiveUser);
                var myLettersCollectionJson = await ActiveServer.SendAndGetAsync(pack);
                var myLettersCollection = JsonConvert.DeserializeObject<List<Letter>>(myLettersCollectionJson);
                if (myLettersCollection != null)
                    return myLettersCollection;
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Добавить картинку на сервер
        /// </summary>
        /// <param name="source">Закодированная картинка</param>
        /// <returns>Возвращает уникальный токен-идентификатор контента</returns>
        public async Task<string> AddSource(Source source)
        {
            try
            {
                var pack = new AddNewSourcePackage(source);
                var imageToken = await ActiveServer.SendAndGetAsync(pack);
                return imageToken;
            }
            catch { }
            return null;
        }
        public async Task<Source> GetSourceByToken(string token)
        {
            try
            {
                var pack = new GetSourceByTokenPackage(token);
                var responseJson = await ActiveServer.SendAndGetAsync(pack);
                var getSource = JsonConvert.DeserializeObject<Source>(responseJson);
                return getSource;
            }
            catch { }
            return null;
        }

    }
    //private JsonSerializerSettings JsonSettings = new JsonSerializerSettings
    //{
    //    TypeNameHandling = TypeNameHandling.Auto,
    //    Formatting = Formatting.Indented
    //};
}
