using System;
using System.Collections.Generic;
using System.Text;

namespace Server_Test
{
    public class Package
    {
        public SendingPackage Pack { get; set; }
        public MetaClass Meta { get; set; }
        public Package(SendingPackage children, MetaClass meta)
        {
            Pack = children;
            Meta = meta;
        }
    }
}
