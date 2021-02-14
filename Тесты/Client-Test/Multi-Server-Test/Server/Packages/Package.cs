using Newtonsoft.Json;

namespace Multi_Server_Test.Server.Packages
{
    public class Package
    {
        //TODO: не работает json constructor
        //[JsonConstructor]
        //public Package(RequestObject sendingObject, SendMeta sendingMeta)
        //{
        //    SendingObject = sendingObject;
        //    SendingMeta = sendingMeta;
        //}
        public object SendingObject { get; set; }
        public SendMeta SendingMeta { get; set; }
    }
}
