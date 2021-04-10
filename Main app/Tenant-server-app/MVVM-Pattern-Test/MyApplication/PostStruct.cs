using Multi_Server_Test.Server;
using MVVM_Pattern_Test.Pages.HomePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.MyApplication
{
    public class PostStruct
    {
        public News ReadNews { get; set; }
        public Page AttachmentsPage { get; set; }
        public PostStruct(News news)
        {
            ReadNews = news;
            ReceiveAttaches();
        }
        public void ReceiveAttaches()
        {
            if (ReadNews?.SourceTokens?.Length < 1) return;
            AttachmentsPage = new AttachmentsPage(ReadNews.SourceTokens);
        }
    }
}
