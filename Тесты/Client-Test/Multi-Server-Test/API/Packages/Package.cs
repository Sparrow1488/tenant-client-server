namespace JumboServer.Packages
{
    public class Package
    {
        public object SendingObject { get; set; }
        public SendMeta SendingMeta { get; set; }
        public string EncryptAES = string.Empty;
    }
}
