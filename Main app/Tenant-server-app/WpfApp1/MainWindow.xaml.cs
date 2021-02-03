using Newtonsoft.Json;
using System;
using System.IO;
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
        private JumboServer server;

        public JumboServer GetServerInstance()
        {
            return server;
        }

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            exceptionLabel_TB.Visibility = Visibility.Collapsed;
            server = new JumboServer(new ServerConfig());
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
                    HomeWindow home = new HomeWindow();
                    HomeWindow.Server = server;
                    home.Show();
                    Close();
                }
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
            catch (ArgumentException ex)
            {
                exceptionLabel_TB.Visibility = Visibility.Visible;
                exceptionLabel_TB.Text = ex.Message;
            }
            finally
            {
                send_Btn.IsEnabled = true;
            }
        }
    }
}
