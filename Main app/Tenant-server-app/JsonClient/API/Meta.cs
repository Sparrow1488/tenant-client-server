using System;
using System.Collections.Generic;
using System.Text;

namespace JsonClient
{
    public class Meta
    {
        public string Action { get; set; }
        public Meta(string action)
        {
            Action = action;
        }
    }
}
