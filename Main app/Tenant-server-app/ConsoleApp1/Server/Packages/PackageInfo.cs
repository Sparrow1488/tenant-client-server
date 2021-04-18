using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Pattern_Test.Server.Packages
{
    public class PackageInfo
    {
        public PackageInfo(int dataSize, string publicRsa)
        {
            PackageLenght = dataSize;
            XmlPublicRsa = publicRsa;
        }
        public bool Encrypted { get; set; } = true;
        public int PackageLenght { get; }
        public string XmlPublicRsa { get; }
    }
}
