using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Packages
{
    public class News
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public News(string title, string desc)
        {
            Title = title;
            Description = desc;
            DateTime = DateTime.Now;
        }

    }
}
