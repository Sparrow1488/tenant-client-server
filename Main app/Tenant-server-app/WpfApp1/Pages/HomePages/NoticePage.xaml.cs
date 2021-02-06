using Multi_Server_Test.Server.Packages;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;
using WpfApp1.Server;
using Newtonsoft.Json;

namespace WpfApp1.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для NoticePage.xaml
    /// </summary>
    public partial class NoticePage : Page
    {
        public NoticePage()
        {
            InitializeComponent();
        }

        private bool IsLoadNews = false;
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsLoadNews)
            {
                var meta = new PackageMeta("127.0.0.1", "news");
                var nullNews = new News();
                var jsonResponse = await HomeWindow.Server.SendAndGet(nullNews, meta);
                var arrayNews = JsonConvert.DeserializeObject<NewsCollection>(jsonResponse);
                foreach (var news in arrayNews.Collection)
                {
                    MessageBox.Show(news.Description, news.Title);
                }
                IsLoadNews = true;
            }
        }
    }
}
