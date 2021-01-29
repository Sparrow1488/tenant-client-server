using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Server.Packages;

namespace WpfApp1.Server.UserInfo
{
    public class Test : RequestObject
    {
        public string Name { get; }
        public Test(string name)
        {
            Name = name;
        }
    }
}
