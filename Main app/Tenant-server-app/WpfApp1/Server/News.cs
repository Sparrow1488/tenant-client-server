using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WpfApp1.Server.Packages;

namespace WpfApp1.Server
{
    public class News : RequestObject
    {
        public List<string> getNews = new List<string>();
        public string Title { get; }
        public string Description { get; }
        public string ImageRef { get; }

        [JsonConstructor]
        public News(string title, string description, string imageRef)
        {
            Title = title;
            Description = description;
            ImageRef = imageRef;
        }
        public News()
        {
            Title = "for request constructor";
            Description = "for request constructor";
        }
    }
}
