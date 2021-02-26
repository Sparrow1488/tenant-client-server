using System;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
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
            try
            {
                var newPerson = new Person(loginBox.Text, passwordBox.Password, 67);
                bool authSuccess = await JumboServer.ActiveServer.AuthorizationAsync(newPerson, false);
                if (authSuccess)
                {
                    new HomeWindow().Show();
                    Close();
                }
                else
                    MessageBox.Show("Ошибка");
            }
            catch (SocketException) { ShowExceptionBlock(exceptionBlock, "Ошибка подключения к серверу"); }
            catch (Exception ex) { ShowExceptionBlock(exceptionBlock, ex.Message); }
            finally { sendBtn.IsEnabled = true; }
        }
        void ShowExceptionBlock(TextBlock block, string message)
        {
            block.Visibility = Visibility.Visible;
            block.Text = message;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ServerConfig config = new ServerConfig();
            new JumboServer(config);
            exceptionBlock.Visibility = Visibility.Collapsed;
        }
    }
}
