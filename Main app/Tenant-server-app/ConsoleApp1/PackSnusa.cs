using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class PackSnusa
    {
        public byte[] SecretData { get; set; }
        public byte[] SecretAesIV { get; set; }
        public byte[] SecretKey{ get; set; }
        public string publicRsaKey { get; set; }
    }
}
