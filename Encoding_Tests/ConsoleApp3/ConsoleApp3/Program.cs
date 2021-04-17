using AesEncryptor;
using RSAEncrypter;
using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;

namespace ConsoleApp3
{
    class Program
    {
        public static RSAParameters PublicServerKey { get; set; }
        public static RSAParameters PublicKey { get; set; }
        public static RSAParameters PrivateKey { get; set; }
        public static byte[] AesKey { get; set; }
        public static byte[] AesIV { get; set; }
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 1090);
            CreateKeys();

            var writeStream = client.GetStream();

            byte[] serverRsa = ReadStreamData(writeStream, "RECEIVE SERVER PUBLIC RSA", 2048);
            string xmlServerRsa = Encoding.UTF32.GetString(serverRsa);
            PublicServerKey = MyRSA.StringToRsa(xmlServerRsa);

            string xmlRsa = MyRSA.RsaToString(PublicKey);
            byte[] publicRsa = Encoding.UTF32.GetBytes(xmlRsa);
            WriteStreamData(writeStream, publicRsa, "RSA PUBLIC KEY SEND");

            byte[] aesKey = EncryptByServerRSA(AesKey);
            WriteStreamData(writeStream, aesKey, "AES KEY SEND");

            byte[] aesIV = EncryptByServerRSA(AesIV);
            WriteStreamData(writeStream, aesIV, "AES IV отправлен");

            byte[] mainData = File.ReadAllBytes(@"C:\Users\DOM\Downloads\header_slilpknot_iowa.jpg");
            string base64Data = Convert.ToBase64String(mainData);
            byte[] encryptMainData = MyAes.Encrypt(base64Data,
                                                                                AesKey,
                                                                                AesIV);
            WriteStreamData(writeStream, encryptMainData, "MAIN DATA SEND");
            client.Close();
        }
        static void WriteStreamData(NetworkStream stream, byte[] data, string consoleMessage)
        {
            while (stream.DataAvailable) { } // Ждем когда сервер прочтет данные
            stream.Write(data, 0, data.Length);
            Console.WriteLine(consoleMessage);
        }
        static byte[] ReadStreamData(NetworkStream stream, string consoleMessage, int bufferLenght)
        {
            while (!stream.DataAvailable) { } // Ждем когда сервер прочтет данные
            byte[] buffer = new byte[bufferLenght];
            stream.Read(buffer, 0, buffer.Length);
            Console.WriteLine(consoleMessage);
            return buffer;
        }
        static void CreateKeys()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            PublicKey = rsa.ExportParameters(false);
            PrivateKey = rsa.ExportParameters(true);
            AesKey = MyAes.GenerateKey();
            AesIV = MyAes.GenerateIV();
        }
        static byte[] EncryptByServerRSA(byte[] key)
        {
            return MyRSA.EncryptData(key, PublicServerKey);
        }
    }
}
