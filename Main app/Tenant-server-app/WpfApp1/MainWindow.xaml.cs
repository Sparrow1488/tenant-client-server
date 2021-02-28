using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.MyApplication;
using WpfApp1.Server;
using WpfApp1.Server.ServerExceptions;
using WpfApp1.Server.ServerMeta;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private JumboServer server;
        private ApplicationEvents application;

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
            errorLabel.Visibility = Visibility.Collapsed;
            server = new JumboServer(new ServerConfig());
            application = new ApplicationEvents();
        }

        private async void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            string errorText;
            errorLabel.Visibility = Visibility.Collapsed;
            send_Btn.IsEnabled = false;
            try
            {
                var sendPerson = new Person(login_TBox.Text, password_TBox.Password, 67);
                bool userSaveToken = saveUserCheckBox.IsChecked.Value;

                var userIsAuthorizate = await server.AuthorizationAsync(sendPerson, userSaveToken);
                if (userIsAuthorizate == true)
                {
                    HomeWindow home = new HomeWindow();
                    home.Show();
                    Close();
                }
            }
            catch (JsonReaderException)
            {
                application.ShowExceptionMessage("Не верный логин или пароль", errorLabel);
            }
            catch (IOException)
            {
                errorText = "Удаленный хост принудительно разорвал существующее подключение";
                application.ShowExceptionMessage(errorText, errorLabel);
            }
            catch (ArgumentException ex)
            {
                application.ShowExceptionMessage(ex.Message, errorLabel);
            }
            catch(JumboServerException ex)
            {
                application.ShowExceptionMessage(ex.Message, errorLabel);
            }
            catch(Exception ex)
            {
                application.ShowExceptionMessage(ex.Message, errorLabel);
            }
            finally
            {
                send_Btn.IsEnabled = true;
            }
        }

        private void iDontKnowPasswordOrLogin_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //TODO: сделать отправку на воостановление доступа
            MessageBox.Show("Запрос полетел администратору. С Вами свяжутся.",
                            "Восстановление логина и пароля", 
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void HideErrorLabel_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            errorLabel.Visibility = Visibility.Collapsed;
        }
        private void MouseHoverTextBlock_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var elem = (TextBlock)sender;
            elem.Foreground = new SolidColorBrush(Colors.Blue);
        }

        private void MouseHoverTextBlock_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var elem = (TextBlock)sender;
            elem.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
