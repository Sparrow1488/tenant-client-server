using Newtonsoft.Json;
using System;

namespace WpfApp1.Server.Packages.SourceDir
{
    public class Source : RequestObject
    {
        public int Id { get; }
        public string Data { get; }
        public int SenderId { get; }
        public DateTime DateCreate { get; } = DateTime.Now;
        [JsonConstructor]
        public Source(int id, string data, int senderId, DateTime dateCreate)
        {
            Id = id;
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
