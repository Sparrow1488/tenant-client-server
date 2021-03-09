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

namespace Chairman_Client.Pages.NewsPageChildren
{
    /// <summary>
    /// Логика взаимодействия для NewsWriterPage.xaml
    /// </summary>
    public partial class NewsWriterPage : Page
    {
        private Functions functions = new Functions("secret", JumboServer.ActiveServer);
        public NewsWriterPage()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NewsPage.ShowNewsPage();
        }
        private async Task<string> PublishNewPost(News news)
        {
            var respone = await functions.AddNews(news);
            return respone;
        }

        private async void SendNewsPostBtn_Click(object sender, RoutedEventArgs e)
        {
            var newTest = new News(titleBox.Text, descBox.Text, JumboServer.ActiveServer.ActiveUser.Id, null, "offer");
            var response = await PublishNewPost(newTest);
            MessageBox.Show(response);
        }
    }
}
