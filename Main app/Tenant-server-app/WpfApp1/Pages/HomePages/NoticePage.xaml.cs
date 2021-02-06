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
        private NewsCollection News;
        public NoticePage()
        {
            InitializeComponent();
        }

        private bool IsLoadNews = false;
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsLoadNews)
            {
                News = await HomeWindow.Server.ReceiveNewsCollection();
                foreach (var news in News.Collection)
                {
                    MessageBox.Show(news.Description, news.Title);
                }
                IsLoadNews = true;
            }
        }
    }
}
