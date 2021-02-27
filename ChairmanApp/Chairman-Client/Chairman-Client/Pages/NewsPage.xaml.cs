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
        public NewsPage()
        {
            InitializeComponent();
        }

        private async void AddNewsBtn_Click(object sender, RoutedEventArgs e)
        {
            var test = new News() { Title = "Тестоый заголовок", Description="Описание", Sender = "asd"};
            var response = await functions.AddNews(test);
            MessageBox.Show(response);
        }
    }
}
