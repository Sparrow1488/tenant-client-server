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
    /// Логика взаимодействия для UniversalLetterPage.xaml
    /// </summary>
    public partial class UniversalLetterPage : Page
    {
        public UniversalLetterPage()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            btn.IsEnabled = false;

            string result = string.Empty;
            try
            {
                var sendLetter = SelectLetterType(titleBox.Text, descBox.Text, JumboServer.ActiveServer.ActiveUser.Id);
                if (sendLetter != null)
                {
                    var letterSender = JumboServer.ActiveServer.ActiveUser.Login;
                    
                    result = await JumboServer.ActiveServer.SendLetter(sendLetter);
                    LetterPage.ShowMessage(result);
                }
            }
            catch (JumboServerException ex)
            {
                LetterPage.ShowExceptionMessage(ex.Message);
            }
            catch (Exception ex)
            {
                LetterPage.ShowExceptionMessage(ex.Message);
            }
            finally
            {
                btn.IsEnabled = true;
            }
        }
        private Letter SelectLetterType(string title, string desc, int senderId)
        {
            if ((bool)offerType.IsChecked)
                return new OfferLetter(title, desc, senderId);
            if ((bool)complaintType.IsChecked)
                return new ComplaintLetter(title, desc, senderId);
            if ((bool)questionType.IsChecked)
                return new QuestionLetter(title, desc, senderId);
            return null;
        }
        private void descBox_MouseEnter(object sender, MouseEventArgs e)
        {
            var textBox = (Border)sender;
            textBox.BorderThickness = new Thickness(1, 0, 1, 0);
            textBox.BorderBrush = new SolidColorBrush(Colors.Purple);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            var textBox = (Border)sender;
            textBox.BorderThickness = new Thickness(0);
        }

    }
}
