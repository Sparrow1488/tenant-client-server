using AesEncryptor;
using RSAEncrypter;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static RSAParameters publicRSA;
        public static RSAParameters privateRSA;

        public static byte[] keyAES;
        public static byte[] IVAES;

        static void Main(string[] args)
        {
            #region Keys creating
            RSACryptoServiceProvider cre = new RSACryptoServiceProvider(1024);
            publicRSA = cre.ExportParameters(false);
            privateRSA = cre.ExportParameters(true);

            keyAES = MyAes.GenerateKey();
            IVAES = MyAes.GenerateIV();
            #endregion

            #region Preparing snus packages
            PackSnusa snus = new PackSnusa();
            snus.SecretData = File.ReadAllBytes(@"C:\Users\DOM\Downloads\kak-pravilno-napisat-elektronoe-pismo.jpg");
            snus.SecretAesIV = IVAES;
            snus.SecretKey = keyAES;
            snus.publicRsaKey = MyRSA.RsaToString(publicRSA);
            #endregion
            #region Creating stream and writing
            MemoryStream stream = new MemoryStream();
            byte[] dataPublicRsa = Encoding.UTF8.GetBytes("капче");
            stream.Write(dataPublicRsa, 0, dataPublicRsa.Length);
            Console.WriteLine("В поток записано: " + stream.Length);
            #endregion

            #region Receiving data from stream
            byte[] _rsaPublicKey = new byte[stream.Length];
            var bytes = stream.Read(_rsaPublicKey, 0, _rsaPublicKey.Length);
            Console.WriteLine(Encoding.UTF8.GetString(_rsaPublicKey));

            #endregion
        }
    }
}
