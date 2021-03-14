using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Server;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.MyApplication.UserInformation
{
    public class User
    {
        public static Person Info { get; set; } = JumboServer.ActiveServer.ActiveUser;
    }
}
