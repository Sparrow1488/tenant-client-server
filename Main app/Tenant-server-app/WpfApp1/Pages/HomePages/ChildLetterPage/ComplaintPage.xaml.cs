using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.MyApplication;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.Packages.LettersDir;
using WpfApp1.Server.ServerExceptions;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Pages.HomePages.ChildLetterPage
{
    /// <summary>
    /// Логика взаимодействия для ComplaintPage.xaml
    /// </summary>
    public partial class ComplaintPage : Page
    {
        public ComplaintPage()
        {
            InitializeComponent();
        }

        private async void sendLetterBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: копипаст кода
            var btn = (Button)sender;
            btn.IsEnabled = false;
            string result = string.Empty;
            try
            {
                var letterSender = JumboServer.ActiveServer.ActiveUser.Login;
                var sendLetter = new ComplaintLetter(titleLetter.Text,
                                                     descriptionLetter.Text,
                                                     letterSender);
                result = await JumboServer.ActiveServer.SendLetter(sendLetter);
                LetterPage.ShowMessage(result);
            }
            catch (SocketException)
            {
                result = "Ошибка подключения: данные не отправлены.";
                LetterPage.ShowExceptionMessage(result);
            }
            catch (IOException)
            {
                result = "Ошибка сервера: данные отправлены, но не могут быть получены.";
                LetterPage.ShowExceptionMessage(result);
            }
            catch (ArgumentException ex)
            {
                LetterPage.ShowExceptionMessage(ex.Message);
            }
            catch (JumboServerException ex)
            {
                LetterPage.ShowExceptionMessage(ex.Message);
            }
            finally
            {
                btn.IsEnabled = true;
            }
        }
    }
}
