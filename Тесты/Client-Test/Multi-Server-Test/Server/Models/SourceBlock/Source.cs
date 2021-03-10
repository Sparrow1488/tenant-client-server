using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Models.SourceBlock
{
    public class Source
    {
        public string Data { get; }
        public string SourceToken { get; }
        public int SenderId { get; }
        public DateTime DateCreate { get; }
        [JsonConstructor]
        public Source(string data, string sourceToken, int senderId, DateTime dateCreate)
        {
            Data = data;
            SourceToken = sourceToken;
            SenderId = senderId;
            DateCreate = dateCreate;
        }
    }
}
