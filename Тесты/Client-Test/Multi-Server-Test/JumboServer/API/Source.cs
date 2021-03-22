using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JumboServer.API
{
    public class Source
    {
        public string Data { get; }
        public string SourceToken { get; }
        public int SenderId { get; }
        public string Extension { get; set; }
        public DateTime DateCreate { get; }
        [JsonConstructor]
        public Source(string data, string sourceToken, int senderId, DateTime dateCreate, string extension)
        {
            Data = data;
            SourceToken = sourceToken;
            SenderId = senderId;
            DateCreate = dateCreate;
            Extension = extension;
        }
    }
}
