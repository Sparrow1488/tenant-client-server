using System;
using System.Net.Sockets;
using System.Windows;
using WpfApp1.Blocks;
using WpfApp1.Classes;
using WpfApp1.Classes.Client.Requests;

namespace WpfApp1
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            excepyionLabel_TB.Visibility = Visibility.Collapsed;
        }

        ServerConfig config = new ServerConfig(ServerConfig.HOST, ServerConfig.PORT);
        private async void sendRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            excepyionLabel_TB.Visibility = Visibility.Collapsed;
            var sendPackage = CreateRequestPackage(CreatePerson(), CreateMeta());
            try
            {
                send_Btn.IsEnabled = false;
                MyServer server = new MyServer(config);
                server.CreateClient();
                await server.SendRequestAsync(sendPackage);
                string response = await server.GetAsync();
                MessageBox.Show(response, "Server response");
            }
            catch (SocketException)
            {
                excepyionLabel_TB.Visibility = Visibility.Visible;
                excepyionLabel_TB.Text = "Не удалось подключиться к серверу";
            }
            finally
            {
                send_Btn.IsEnabled = true;
            }
        }
        private Person CreatePerson()
        {
            return new Person()
            {
                Login = login_TBox.Text,
                Password = password_TBox.Password
            };
        }
        private Meta CreateMeta()
        {
            return new Meta()
            {
                Address = "127.0.0.1",
                Action = "auth"
            };
        }
        private IRequest CreateRequestPackage(IForRequest request, Meta meta)
        {
            return new PersonRequest(request, meta);
        }

    }
}
