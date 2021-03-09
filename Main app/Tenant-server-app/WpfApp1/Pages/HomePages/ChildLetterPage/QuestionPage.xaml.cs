using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerExceptions;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Pages.HomePages.ChildLetterPage
{
    /// <summary>
    /// Логика взаимодействия для QuestionPage.xaml
    /// </summary>
    public partial class QuestionPage : Page
    {
        public QuestionPage()
        {
            InitializeComponent();
        }

        private async void sendLetterBtn_Click(object sender, RoutedEventArgs e)
        {
            //var btn = (Button)sender;
            //btn.IsEnabled = false;
            //string result = string.Empty;
            //try
            //{
            //    var letterSender = JumboServer.ActiveServer.ActiveUser.Login;
            //    var sendLetter = new QuestionLetter(titleLetter.Text,
            //                                     descriptionLetter.Text,
            //                                     letterSender);
            //    result = await JumboServer.ActiveServer.SendLetter(sendLetter);
            //    LetterPage.ShowMessage(result);
            //}
            //catch (SocketException)
            //{
            //    result = "Ошибка подключения: данные не отправлены.";
            //    LetterPage.ShowExceptionMessage(result);
            //}
            //catch (IOException)
            //{
            //    result = "Ошибка сервера: данные отправлены, но не могут быть получены.";
            //    LetterPage.ShowExceptionMessage(result);
            //}
            //catch (ArgumentException ex)
            //{
            //    LetterPage.ShowExceptionMessage(ex.Message);
            //}
            //catch (JumboServerException ex)
            //{
            //    LetterPage.ShowExceptionMessage(ex.Message);
            //}
            //finally
            //{
            //    btn.IsEnabled = true;
            //}
        }
    }
}
