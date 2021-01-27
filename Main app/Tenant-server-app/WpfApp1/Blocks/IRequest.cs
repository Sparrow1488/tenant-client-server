using System;
using WpfApp1.Classes;
using Newtonsoft.Json;

namespace WpfApp1.Blocks
{
    public abstract class IRequest
    {
        public string JsonRequest = null;
        public IForRequest SendingObject { get; set; }
        public Meta SendingMeta { get; set; }
        public IRequest(IForRequest request, Meta meta)
        {
            SendingObject = request;
            SendingMeta = meta;
            JsonRequest = JsonConvert.SerializeObject(this);
        }
    }
    public abstract class IForRequest
    {
        public IRequest SendingObject { get; set; }
        public Meta SendingMeta { get; set; }
    }
}
