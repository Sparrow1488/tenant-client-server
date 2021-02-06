using System;
using System.Collections.Generic;
using System.Text;
using WpfApp1.Server.Packages;

namespace Multi_Server_Test.Server.Packages
{
    public class News : RequestObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageRef { get; set; }
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
    }
}
