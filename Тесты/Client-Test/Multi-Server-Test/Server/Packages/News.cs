using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Packages
{
    public class News
    {
        //public static List<News> sendNewsList = new List<News>();
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageRef { get; set; }
        public News(string title, string desc)
        {
            //sendNewsList.Add(this);
            Title = title;
            Description = desc;
        }

    }
}
