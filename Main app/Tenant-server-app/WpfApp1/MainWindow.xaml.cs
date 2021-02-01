using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using WpfApp1.Classes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private JamboServer server;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            exceptionLabel_TB.Visibility = Visibility.Collapsed;
            server = new JamboServer(new ServerConfig());
        }

        private async void authBtn_Click(object sender, RoutedEventArgs e)
        {
            exceptionLabel_TB.Visibility = Visibility.Collapsed;
            send_Btn.IsEnabled = false;

            try
            {
                var sendPerson = new Person(login_TBox.Text, password_TBox.Password, 67);
                
                var userIsAuthorizate = await server.Authorization(sendPerson);
                if (userIsAuthorizate)
                {
                    exceptionLabel_TB.Visibility = Visibility.Visible;
                    exceptionLabel_TB.Text = "Добро пожаловать!";
                }
                server.ShowUserInfo();
            }
            catch (SocketException)
            {
                exceptionLabel_TB.Visibility = Visibility.Visible;
                exceptionLabel_TB.Text = "Не удалось подключиться к серверу";
            }
            catch (JsonReaderException)
            {
                MessageBox.Show("Получены нечитаемые данные!", "Exception");
            }
            catch (IOException)
            {
                exceptionLabel_TB.Visibility = Visibility.Visible;
                exceptionLabel_TB.Text = "Удаленный хост принудительно разорвал существующее подключение";
            }
            finally
            {
                send_Btn.IsEnabled = true;
            }
        }
    }
}
