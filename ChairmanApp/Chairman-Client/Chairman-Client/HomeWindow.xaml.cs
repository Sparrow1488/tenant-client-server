using Chairman_Client.Pages;
using Chairman_Client.Server.Chairman.Functions;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private Page letterPage = new LetterPage();
        public HomeWindow()
        {
            InitializeComponent();
        }

        private void SelectLetterPageBtn_Click(object sender, RoutedEventArgs e)
        {
            homeContentFrame.Content = letterPage;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var func = new Functions("secret", JumboServer.ActiveServer);
            MessageBox.Show("Welcome!");
        }
    }
}
