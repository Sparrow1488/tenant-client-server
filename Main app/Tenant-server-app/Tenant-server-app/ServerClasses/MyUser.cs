using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenant_server_app.ServerClasses
{
    public class MyUser
    {
        public static MyUser Active = null;
        public string Login = null;
        public string Password = null;
        public MyUser(string login, string pass)
        {
            Login = login;
            Password = pass;
        }
    }
}
