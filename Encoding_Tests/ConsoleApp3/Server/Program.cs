using AesEncryptor;
using RSAEncrypter;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        public static RSAParameters PublicClientKey { get; set; }
        public static RSAParameters PublicKey { get; set; }
        public static RSAParameters PrivateKey { get; set; }
        public static byte[] AesKey { get; set; }
        public static byte[] AesIV { get; set; }
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(1090);
            CreateKeys();
            listener.Start();
            while (true)
            {
                Console.WriteLine("Ожидаю");
                var client = listener.AcceptTcpClient();
                Task.Run(()=>ReceiveClient(client));
            }
        }
        public static void ReceiveClient(TcpClient client)
        {
            var stream = client.GetStream();

            string xmlRsa = MyRSA.RsaToString(PublicKey);
            byte[] publicRsa = Encoding.UTF32.GetBytes(xmlRsa);
            stream.Write(publicRsa, 0, publicRsa.Length);

            while (!stream.DataAvailable) { }
            byte[] key = new byte[2048];
            stream.Read(key, 0, key.Length);
            var rsa = Encoding.UTF32.GetString(key);
            Console.WriteLine(rsa);
            stream.Flush();

            while (!stream.DataAvailable) { }
            byte[] aesKey = new byte[128];
            stream.Read(aesKey, 0, aesKey.Length);
            Console.WriteLine("Aes Key: " + aesKey);
            stream.Flush();

            while (!stream.DataAvailable) { }
            byte[] aesIV = new byte[128];
            stream.Read(aesIV, 0, aesIV.Length);
            Console.WriteLine("Aes IV: " + aesIV);
            stream.Flush();

            byte[] decAesKey = DecryptByRsa(aesKey);
            byte[] decAesIV = DecryptByRsa(aesIV);

            while (!stream.DataAvailable) { }
            byte[] encryptMainData = new byte[500000];
            stream.Read(encryptMainData, 0, encryptMainData.Length);
            var base64Image = MyAes.Decrypt(encryptMainData, decAesKey, decAesIV);
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            var image = Image.FromStream(new MemoryStream(imageBytes));
            Console.WriteLine("image: " + image.Width);
            stream.Flush();

        }
        static void CreateKeys()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            PublicKey = rsa.ExportParameters(false);
            PrivateKey = rsa.ExportParameters(true);
            AesKey = MyAes.GenerateIV();
            AesIV = MyAes.GenerateIV();
        }
        static byte[] DecryptByRsa(byte[] encKey)
        {
            return MyRSA.DecryptData(encKey, PrivateKey);
        }
    }
}
