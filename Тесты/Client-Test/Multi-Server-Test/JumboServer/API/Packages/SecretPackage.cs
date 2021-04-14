using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumboServer.API.Packages
{
    public class SecretPackage
    {
        public byte[] KEY { get; set; }
        public byte[] IV { get; set; }
        public byte[] EncyptPackage { get; set; }
    }
}
