using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Packages
{
    public class NewsList
    {
        public List<object> newsList = new List<object>();
        public NewsList(News n, News ews)
        {
            newsList.Add(n);
            newsList.Add(ews);
        }
    }
}
