using Multi_Server_Test.Server;
using WpfApp1.Server.ServerMeta;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using WpfApp1.Server.ServerExceptions;
using MVVM_Pattern_Test.ViewModels.NewsViewModels;

namespace WpfApp1.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для NoticePage.xaml
    /// </summary>
    public partial class NoticePage : Page
    {
        private List<News> ReceivedNews;
        public NoticePage()
        {
            InitializeComponent();
            DataContext = new RecieveNewsVM();
        }

        //private bool IsLoadNews = false;
        //private async void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (!IsLoadNews)
        //        {
        //            ReceivedNews = await JumboServer.ActiveServer.ReceiveNewsCollectionAsync();
        //            foreach (var news in ReceivedNews)
        //            {
        //                AddNewsPanel(news);
        //            }
        //            IsLoadNews = true;
        //        }
        //    }
        //    catch (JumboServerException ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString(),
        //                        "Внутреннее исключение",
        //                        MessageBoxButton.OK,
        //                        MessageBoxImage.Error);
        //    }
        //}
    }
}
