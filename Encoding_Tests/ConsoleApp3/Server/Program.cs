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
        //public static byte[] AesKey { get; set; }
        //public static byte[] AesIV { get; set; }
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

            byte[] publicClientKey = ReadStreamData(stream, "PUBLIC KEY GET", 2048);
            byte[] encKey  = ReadStreamData(stream, "AES KEY GET", 128);
            byte[] encIV = ReadStreamData(stream, "AES IV GET", 128);
            //byte[] encMainData = ReadStreamData(stream, "MAIN DATA GET", 49);
            byte[] decAesKey = DecryptByRsa(encKey);
            byte[] decAesIV = DecryptByRsa(encIV);

            string base64Image = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                byte[] encDataBytes = ms.ToArray();
                base64Image = MyAes.Decrypt(encDataBytes, decAesKey, decAesIV);
            }
            byte[] finalyDataBytes = Convert.FromBase64String(base64Image);
            File.WriteAllBytes(@"C:\Users\DOM\Downloads\data.jpg", finalyDataBytes);
            Console.WriteLine("DATA WRITE");
        }
        static void CreateKeys()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            PublicKey = rsa.ExportParameters(false);
            PrivateKey = rsa.ExportParameters(true);
            //AesKey = MyAes.GenerateKey();
            //AesIV = MyAes.GenerateIV();
        }
        static byte[] DecryptByRsa(byte[] encKey)
        {
            return MyRSA.DecryptData(encKey, PrivateKey);
        }
        static byte[] ReadStreamData(NetworkStream stream, string consoleMessage, int bufferLenght)
        {
            while (!stream.DataAvailable) { } // Ждем когда сервер прочтет данные
            byte[] buffer = new byte[bufferLenght];
            stream.Read(buffer, 0, buffer.Length);
            Console.WriteLine(consoleMessage);
            stream.Flush();
            return buffer;
        }
    }
}
