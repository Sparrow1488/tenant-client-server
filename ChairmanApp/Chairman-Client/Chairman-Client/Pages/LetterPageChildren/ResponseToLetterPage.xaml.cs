using Chairman_Client.Server.Chairman.Functions;
using Chairman_Client.Server.Packages.LettersDir;
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
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client.Pages.LetterPageChildren
{
    /// <summary>
    /// Логика взаимодействия для ResponseToLetterPage.xaml
    /// </summary>
    public partial class ResponseToLetterPage : Page
    {
        private Functions serverFunctions = new Functions("secret", JumboServer.ActiveServer);
        private Letter ResponseLetter = null;
        public ResponseToLetterPage(Letter responseLetter)
        {
            InitializeComponent();

            ResponseLetter = responseLetter;
        }

        private async void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(responseTextBox.Text))
                {
                    var reply = new ReplyLetter(responseTextBox.Text, null, JumboServer.ActiveServer.ActiveUser.Login, ResponseLetter.Id);
                    var response = await serverFunctions.SendReplyToLetter(reply);
                    MessageBox.Show(response);
                }
            }
            catch { MessageBox.Show("Внутренняя ошибка"); }
        }
    }
}
