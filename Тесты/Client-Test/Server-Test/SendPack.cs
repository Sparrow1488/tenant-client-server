using System;
using System.Collections.Generic;
using System.Text;

namespace Server_Test
{
    public class SendPack : Package
    {
        public SendPack(SendingPackage children, MetaClass meta) : base(children, meta)
        {
        }
    }
}
