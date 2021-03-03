using Chairman_Client.Pages.NewsPageChildren;
using Chairman_Client.Server.Chairman.Functions;
using Multi_Server_Test.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Server.ServerExceptions;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewsPage.xaml
    /// </summary>
    public partial class NewsPage : Page
    {
        private Functions functions = new Functions("secret", JumboServer.ActiveServer);
        private bool newsIsLoaded = false;
        private Page newsWritePage = new NewsWriterPage();
        public NewsPage()
        {
            InitializeComponent();
        }

        private async void AddNewsBtn_Click(object sender, RoutedEventArgs e)
        {
            moreActionsFrame.Content = newsWritePage;
        }
        

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(newsIsLoaded == false)
            {
                List<News> recivedNews = null;
                try
                {
                    recivedNews = await JumboServer.ActiveServer.ReceiveNewsCollectionAsync();
                }
                catch (JumboServerException) { }

                if (recivedNews != null)
                {
                    moreActionsFrame.Content = new NewsCollectionPage(recivedNews);
                    
                    newsIsLoaded = true;
                    MessageBox.Show("Все новости успешно получены!", "Отчет о загрузке");
                }
            }
        }
    }
}
