using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Models.SourceBlock
{
    public class Source
    {
        public int Id { get; }
        public string Data { get; }
        public int SenderId { get; }
        public DateTime DateCreate { get; }
        [JsonConstructor]
        public Source(int id, string data, int senderId, DateTime dateCreate)
        {
            Id = id;
            Data = data;
            SenderId = senderId;
            DateCreate = dateCreate;
        }
    }
}
