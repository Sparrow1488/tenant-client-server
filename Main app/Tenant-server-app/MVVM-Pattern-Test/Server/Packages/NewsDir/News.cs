using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using WpfApp1.Server.Packages;

namespace Multi_Server_Test.Server
{
    public class News : RequestObject
    {
        public string Title { get; set; } //TODO: инкапсуляция
        public int Id { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Sender { get; set; }
        public int SenderId { get; set; }
        public string Type { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        [JsonConstructor]
        public News(int id, string title, string description, string sender, string source, string type, DateTime dateTime, int senderId)
        {
            Id = id;
            Title = title;
            Description = description;
            DateTime = dateTime;
            Source = source;
            Sender = sender;
            Type = type;
            SenderId = senderId;
        }
        public News(string title, string description, int senderId, string source, string type)
        {
            Title = title;
            Title = title;
            Description = description;
            SenderId = senderId;
            Source = source;
            Type = type;
        }
        public News() { }
    }
}
