using System;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Server;
using WpfApp1.Server.Packages.PersonalDir;
using WpfApp1.Server.ServerExceptions;
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

        private async void AuthPanel_Click(object sender, RoutedEventArgs e)
        {
            ShowMessageBlock(exceptionBlock, "Отправка...");
            var sendBtn = (Border)sender;
            sendBtn.IsEnabled = false;
            try
            {
                var newPerson = new Person(loginBox.Text, passwordBox.Password, 67);
                bool authSuccess = await JumboServer.ActiveServer.AuthorizationAsync(newPerson, true);
                if (authSuccess)
                    OpenHomeWindow();
                else
                    MessageBox.Show("Ошибка");
            }
            catch (SocketException) { ShowExceptionBlock(exceptionBlock, "Ошибка подключения к серверу"); }
            catch (Exception ex) { ShowExceptionBlock(exceptionBlock, ex.Message); }
            finally { sendBtn.IsEnabled = true; }
        }
        private void OpenHomeWindow()
        {
            new HomeWindow().Show();
            Close();
        }
        void ShowExceptionBlock(TextBlock block, string message)
        {
            block.Foreground = new SolidColorBrush(Colors.Red);
            block.Visibility = Visibility.Visible;
            block.Text = message;
        }
        void ShowMessageBlock(TextBlock block, string message)
        {
            block.Foreground = new SolidColorBrush(Colors.Black);
            block.Visibility = Visibility.Visible;
            block.Text = message;
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ServerConfig config = new ServerConfig();
            var server = new JumboServer(config);
            exceptionBlock.Visibility = Visibility.Collapsed;

            UserToken token = null;
            bool authResult = false;
            token = server.DeserializeTokenByFileName(server.tokenFileName);
            try
            {
                if (token != null)
                    authResult = await JumboServer.ActiveServer.AuthorizationByTokenAsync(token);
                if (authResult)
                    OpenHomeWindow();
            }
            catch (JumboServerException) { }
            catch { }
        }
    }
}
