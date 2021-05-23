using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Pattern_Test.Server.Packages
{
    public class SecretPackage
    {
        public byte[] KEY { get; set; }
        public byte[] IV { get; set; }
        public byte[] EncyptPackage { get; set; }
    }
}
