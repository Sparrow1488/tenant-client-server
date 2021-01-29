using System;
using System.Collections.Generic;
using System.Text;

namespace Server_Test
{
    public class MetaClass
    {
        public string StringMeta { get; set; }
        public MetaClass(string name)
        {
            StringMeta = name;
        }
    }
}
