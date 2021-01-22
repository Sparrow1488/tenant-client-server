using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class MyUser
    {
        public string Login = null;
        public string Password = null;
        public int ID;
        public MyUser(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
