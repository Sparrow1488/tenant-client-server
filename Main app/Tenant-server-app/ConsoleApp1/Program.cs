using AesEncryptor;
using Chairman_Client.Chairman.Packages;
using Multi_Server_Test.Server;
using Newtonsoft.Json;
using RSAEncrypter;
using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Server;
using WpfApp1.Server.Packages.PersonalDir;
using WpfApp1.Server.ServerMeta;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            JumboServer act = new JumboServer(new ServerConfig());
            TcpClient client = new TcpClient(new ServerConfig().HOST, new ServerConfig().PORT);
            var connected = await act.TryConnect(client);
            await act.SendAndGetAsync(new AuthorizationPackage(new Person("", "")));

            //string jsonResponse = null;
            //RSACryptoServiceProvider src = new RSACryptoServiceProvider();
            //RSAParameters publicKey = src.ExportParameters(false);
            //RSAParameters privateKey = src.ExportParameters(true);
            //byte[] aesKey = MyAes.GenerateKey();
            //byte[] aesIV = MyAes.GenerateIV();
            //if (connected)
            //{
            //    var stream = client.GetStream();
            //    byte[] serverRsaData = await act.GetResponseAsync(stream); // GET RSA
            //    string xmlServerRsa = JumboServer.ActiveServer.ServerEncoding.GetString(serverRsaData);
            //    RSAParameters serverPublicRsa = MyRSA.StringToRsa(xmlServerRsa);
            //    byte[] clientRsaData = JumboServer.ActiveServer.ServerEncoding.GetBytes(MyRSA.RsaToString(publicKey));
            //    act.SendRequest(clientRsaData, stream); // PUBLIC RSA
            //    act.SendRequest(MyRSA.EncryptData(aesKey, serverPublicRsa), stream); // AES KEY
            //    act.SendRequest(MyRSA.EncryptData(aesIV, serverPublicRsa), stream); // AES IV
            //    act.SendRequest(MyRSA.EncryptData(aesIV, serverPublicRsa), stream); // AES IV
            //    //string jsonPackage = JsonConvert.SerializeObject(new AuthorizationPackage(new Person("", ""))) ;
            //    //byte[] encJsonPackage = MyAes.Encrypt(jsonPackage, aesKey, aesIV);
            //    //await act.SendRequest(encJsonPackage, stream); // MAIN DATA

            //    //byte[] response = await act.GetResponseAsync(stream);
            //}



            //#region Keys creating
            //RSACryptoServiceProvider cre = new RSACryptoServiceProvider(1024);
            //publicRSA = cre.ExportParameters(false);
            //privateRSA = cre.ExportParameters(true);

            //keyAES = MyAes.GenerateKey();
            //IVAES = MyAes.GenerateIV();
            //#endregion

            //#region Preparing snus packages
            //PackSnusa snus = new PackSnusa();
            //snus.SecretData = File.ReadAllBytes(@"C:\Users\DOM\Downloads\kak-pravilno-napisat-elektronoe-pismo.jpg");
            //snus.SecretAesIV = IVAES;
            //snus.SecretKey = keyAES;
            //snus.publicRsaKey = MyRSA.RsaToString(publicRSA);
            //#endregion
            //#region Creating stream and writing
            //MemoryStream stream = new MemoryStream();
            //byte[] dataPublicRsa = Encoding.UTF8.GetBytes("капче");
            //stream.Write(dataPublicRsa, 0, dataPublicRsa.Length);
            //Console.WriteLine("В поток записано: " + stream.Length);
            //#endregion

            //#region Receiving data from stream
            //byte[] _rsaPublicKey = new byte[stream.Length];
            //var bytes = stream.Read(_rsaPublicKey, 0, _rsaPublicKey.Length);
            //Console.WriteLine(Encoding.UTF8.GetString(_rsaPublicKey));

            //#endregion
        }
    }
}
