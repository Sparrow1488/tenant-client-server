using Newtonsoft.Json;
using System;

namespace WpfApp1.Server.Packages.SourceDir
{
    public class Source : RequestObject
    {
        public string SourceToken { get; }
        public string Data { get; }
        public int SenderId { get; }
        public DateTime DateCreate { get; } = DateTime.Now;
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
