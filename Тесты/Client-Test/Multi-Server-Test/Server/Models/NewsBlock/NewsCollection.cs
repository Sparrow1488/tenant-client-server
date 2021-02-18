using System;
using System.Collections.Generic;

namespace Multi_Server_Test.Server.Packages
{
    public class NewsCollection
    {
        public static string Name = "Multi-list-news";
        public List<News> Collection = new List<News>();
        public NewsCollection(List<News> listNews)
        {
            if(listNews != null)
            {
                Collection = listNews;
            }
        }
    }
}
