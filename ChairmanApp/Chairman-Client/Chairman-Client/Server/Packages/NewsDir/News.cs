using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using WpfApp1.Server.Packages;

namespace Multi_Server_Test.Server
{
    public class News : RequestObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
    }
}
