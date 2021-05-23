using ExchangeSystem.Requests.Objects.Entities;
using MVVM_Pattern_Test.Pages.HomePages;
using System.Windows.Controls;

namespace MVVM_Pattern_Test.MyApplication
{
    public class PostStruct
    {
        public Publication ReadPublication { get; set; }
        public Page AttachmentsPage { get; set; }
        public PostStruct(Publication news)
        {
            ReadPublication = news;
            ReceiveAttaches();
        }
        public void ReceiveAttaches()
        {
            //if (ReadNews?.SourceTokens?.Length < 1) return;
            //AttachmentsPage = new AttachmentsPage(ReadNews.SourceTokens);
        }
    }
}
