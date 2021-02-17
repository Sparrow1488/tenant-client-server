using Multi_Server_Test.Server.Packages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Blocks
{
    public class Person : RequestObject
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ParentName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Room { get; set; }
        public int ID { get; set; }

        //[JsonConstructor]
        //public Person(string login, string password)
        //{
        //    Login = login;
        //    Password = password;
        //}
    }
}
