using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace JsonClient
{
    public class Person : IRequestObj
    {
        public Person(string name, string pass)
        {
            Name = name;
            Password = pass;
        }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
