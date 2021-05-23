using Newtonsoft.Json;
using System;
using WpfApp1.Server.Packages;

namespace Multi_Server_Test.Server
{
    public class News : RequestObject
    {
        public string Title { get; set; } //TODO: инкапсуляция
        public int Id { get; set; }
        public string Description { get; set; }
        public string[] SourceTokens { get; set; }
        public string Sender { get; set; }
        public int SenderId { get; set; }
        public string Type { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        [JsonConstructor]
        public News(int id, string title, string description, string sender, string[] sourceTokens, string type, DateTime dateTime, int senderId)
        {
            Id = id;
            Title = title;
            Description = description;
            DateTime = dateTime;
            SourceTokens = sourceTokens;
            Sender = sender;
            Type = type;
            SenderId = senderId;
        }
        public News(string title, string description, int senderId, string[] sourceTokens, string type)
        {
            Title = title;
            Title = title;
            Description = description;
            SenderId = senderId;
            SourceTokens = sourceTokens;
            Type = type;
        }
        public News() { }
    }
}
