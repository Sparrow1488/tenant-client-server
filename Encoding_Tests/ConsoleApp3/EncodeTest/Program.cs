using AesEncryptor;
using RSAEncrypter;
using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;

namespace EncodeTest
{
    class Program
    {
        public static RSAParameters PublicKey { get; set; }
        public static RSAParameters PrivateKey { get; set; }
        public static byte[] AesKey { get; set; }
        public static byte[] AesIV { get; set; }
        static void Main(string[] args)
        {
            CreateKeys();
            byte[] encKey = EncryptKey(AesKey, PublicKey);
            byte[] encIV = EncryptKey(AesKey, PublicKey);

            byte[] decKey = DecryptKey(encKey, PrivateKey);
            byte[] decIV = DecryptKey(encIV, PrivateKey);

            byte[] startData = File.ReadAllBytes(@"C:\Users\Dom\Downloads\f9939ca7338bb3bbd8dab55007cb7884.mp4");
            string baseStartData = Convert.ToBase64String(startData);
            var encryptStartData = MyAes.Encrypt(baseStartData, decKey, decIV);

            string decryptStartData = MyAes.Decrypt(encryptStartData, decKey, decIV);
            byte[] finalyData = Convert.FromBase64String(decryptStartData);
            File.WriteAllBytes(@"C:\Users\Dom\Downloads\чепапапа.mp4", finalyData);
            Console.WriteLine("FINALY");
        }
        static void CreateKeys()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            PublicKey = rsa.ExportParameters(false);
            PrivateKey = rsa.ExportParameters(true);
            AesKey = MyAes.GenerateIV();
            AesIV = MyAes.GenerateIV();
        }
        static byte[] EncryptKey(byte[] key, RSAParameters pubRsa)
        {
            return MyRSA.EncryptData(key, pubRsa);
        }
        static byte[] DecryptKey(byte[] decKey, RSAParameters privRsa)
        {
            return MyRSA.DecryptData(decKey, privRsa);
        }
    }
}
