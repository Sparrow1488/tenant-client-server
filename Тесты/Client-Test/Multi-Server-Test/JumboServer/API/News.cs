using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JumboServer.API
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string[] SourceTokens { get; set; }

        public string Sender { get; set; }
        public int SenderId { get; set; }

        public string Type { get; set; }
        public DateTime DateTime { get; set; } = DateTime.MinValue;
        public News(string title, string desc, DateTime date)
        {
            Title = title;
            Description = desc;
            DateTime = DateTime.Now;
            DateTime = date;
        }
        [JsonConstructor]
        public News(int id, string title, string description, string[] sourceTokens, string sender, string type, DateTime dateTime, int senderId)
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
        public override string ToString()
        {
            return $"{Title}: {Description} <{DateTime.ToLongDateString()}>";
        }

    }
}
