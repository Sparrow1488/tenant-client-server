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
using WpfApp1.Server;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var sendBtn = (Button)sender;
            sendBtn.IsEnabled = false;
            var newPerson = new Person(loginBox.Text, passwordBox.Text, 67);
            try
            {
                bool authSuccess = await JumboServer.ActiveServer.AuthorizationAsync(newPerson, false);
                if (authSuccess)
                {
                    new HomeWindow().Show();
                    Close();
                }
                else
                    MessageBox.Show("Ошибка");
                sendBtn.IsEnabled = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Exception dialog"); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ServerConfig config = new ServerConfig();
            new JumboServer(config);
        }
    }
}
