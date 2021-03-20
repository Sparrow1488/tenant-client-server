using Newtonsoft.Json;
using System;

namespace WpfApp1.Server.Packages.SourceDir
{
    public class Source : RequestObject
    {
        public string SourceToken { get; set; }
        public string Data { get; set; }
        public int SenderId { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        [JsonConstructor]
        public Source(string data, string sourceToken, int senderId, DateTime dateCreate)
        {
            SourceToken = sourceToken;
            Data = data;
            SenderId = senderId;
            DateCreate = dateCreate;
        }
        public Source(string data, int senderId)
        {
            Data = data;
            SenderId = senderId;
        }
    }
}
