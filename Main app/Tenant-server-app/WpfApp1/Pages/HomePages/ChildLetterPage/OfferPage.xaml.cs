using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Pages.HomePages.ChildLetterPage
{
    /// <summary>
    /// Логика взаимодействия для OfferPage.xaml
    /// </summary>
    public partial class OfferPage : Page
    {
        public OfferPage()
        {
            InitializeComponent();
        }

        private async void sendLetterBtn_Click(object sender, RoutedEventArgs e)
        { //TODO: копипаст кода
            var btn = (Button)sender;
            btn.IsEnabled = false;
            string result = string.Empty;
            try
            {
                var letterSender = JumboServer.ActiveServer.ActiveUser;
                var sendLetter = new OfferLetter(titleLetter.Text,
                                                 descriptionLetter.Text,
                                                 letterSender);
                result = await JumboServer.ActiveServer.SendLetter(sendLetter);
            }
            catch (SocketException)
            {
                result = "Ошибка подключения: данные не отправлены.";
            }
            catch (IOException)
            {
                result = "Ошибка сервера: данные отправлены, но не могут быть получены.";
            }
            finally
            {
                LetterPage.ShowExceptionText(result);
                btn.IsEnabled = true;
            }
        }
    }
}
