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
        public string Description { get; set; }
        public string Source { get; set; }
        public string Sender { get; set; }
        public string Type { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        [JsonConstructor]
        public News(string title, string description, string sender, string source, string type, DateTime dateTime)
        {
            Title = title;
            Description = description;
            DateTime = dateTime;
            Source = source;
            Sender = sender;
            Type = type;
        }
        public News(string title, string description, string sender)
        {
            Title = title;
            Description = description;
            Sender = sender;
        }
    }
}
