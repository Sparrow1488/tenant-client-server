namespace MVVM_Pattern_Test.Server.Packages.Meta
{
    public struct HouseKeeper
    {
        public byte[] AesKey { get; set; }
        public byte[] AesIV { get; set; }
        public string PublicXmlKey { get; set; }
        public string PrivateXmlKey { get; set; }
    }
}
