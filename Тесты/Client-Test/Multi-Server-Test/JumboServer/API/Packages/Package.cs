using Newtonsoft.Json;

namespace JumboServer.Packages
{
    public class Package
    {
        //TODO: не работает json constructor
        public object SendingObject { get; set; }
        public SendMeta SendingMeta { get; set; }
    }
}
