using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Packages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test
{
    public class PersonRequest : Package
    {
        public PersonRequest(RequestObject request, SendMeta meta) : base(request, meta)
        {

        }
        //public Person DataPerson { get; set; }
        //public SendMeta DataMeta { get; set; }
    }
}
