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
        public NewsPage()
        {
            InitializeComponent();
        }

        private async void AddNewsBtn_Click(object sender, RoutedEventArgs e)
        {
            var test = new News("Тестоый заголовок", "Описание", JumboServer.ActiveServer.ActiveUser.Login);
            var response = await functions.AddNews(test);
            MessageBox.Show(response);
        }
        private StackPanel CreateNewsPanel(News news)
        {
            var mainPanel = new StackPanel() { Margin = new Thickness(0, 10, 0, 0) };
            var titleBlock = new TextBlock()
            {
                FontSize = 30, 
                FontWeight = FontWeights.DemiBold,
                TextWrapping = TextWrapping.Wrap,
                Text = news.Title,
                FontFamily = new FontFamily("Calibri"),
            };

            var infoNewsPanel = new StackPanel();
            var authorBlock = new TextBlock()
            {
                FontSize = 22,
                FontStyle = FontStyles.Italic,
                Text = "Author: " + news.Sender,
                FontFamily = new FontFamily("Calibri")
            };
            var typeBlock = new TextBlock()
            {
                FontSize = 22,
                FontStyle = FontStyles.Italic,
                Text = "Type: " + news.Type,
                FontFamily = new FontFamily("Calibri")
            };
            infoNewsPanel.Children.Add(authorBlock);
            infoNewsPanel.Children.Add(typeBlock);

            var descBlock = new TextBlock()
            {
                FontSize = 28,
                TextWrapping = TextWrapping.Wrap,
                Text = news.Description,
                FontFamily = new FontFamily("Calibri Light")
            };
            mainPanel.Children.Add(titleBlock);
            mainPanel.Children.Add(infoNewsPanel);
            mainPanel.Children.Add(descBlock);

            return mainPanel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(newsIsLoaded == false)
            {
                var recivedNews = await JumboServer.ActiveServer.ReceiveNewsCollectionAsync();
                foreach (var news in recivedNews)
                {
                    var newsPanel = CreateNewsPanel(news);
                    newsMainPanel.Children.Add(newsPanel);
                }
                newsIsLoaded = true;
                MessageBox.Show("Все новости успешно получены!", "Отчет о загрузке");
            }
        }
    }
}
