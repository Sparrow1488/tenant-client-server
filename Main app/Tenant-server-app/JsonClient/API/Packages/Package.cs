using System;
using System.Collections.Generic;
using System.Text;

namespace JsonClient
{
    public class Package<T>
        where T: IRequestObj
    {
        public Package(IRequestObj send, Meta meta)
        {
            SendObject = (T)send;
            SendMeta = meta;
        }
        public T SendObject { get; set; }
        public Meta SendMeta { get; set; }
    }
}
