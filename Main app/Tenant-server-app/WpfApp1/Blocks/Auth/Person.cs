using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Blocks;

namespace WpfApp1.Classes
{
    public class Person : IForRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ParentName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Room { get; set; }
        public int ID { get; set; }
    }
}
