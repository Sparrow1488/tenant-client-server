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

namespace WpfApp1.Pages.HomePages.ChildLetterPage
{
    /// <summary>
    /// Логика взаимодействия для ReplyReaderPage.xaml
    /// </summary>
    public partial class ReplyReaderPage : Page
    {
        private List<Letter> MySendLetters = new List<Letter>();
        public ReplyReaderPage(List<Letter> sendLetters)
        {
            InitializeComponent();

            MySendLetters = sendLetters;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(MySendLetters != null)
            {
                foreach (var letter in MySendLetters)
                {
                    AddMyLetterOnPanel(letter);
                }
            }
        }
        private void AddMyLetterOnPanel(Letter letter)
        {
            var mainPanel = new StackPanel()
            {
                Margin = new Thickness(15, 2, 15, 2),
            };
            var titleBlock = new TextBlock()
            {
                FontSize = 24,
                FontFamily = new FontFamily("Calibri Light"),
                Text = "Заголовок: " + letter.Title
            };
            var dateBlock = new TextBlock()
            {
                FontSize = 24,
                FontFamily = new FontFamily("Calibri Light"),
                Text = "Дата отправки: " + letter.DateCreate.ToShortDateString()
            };
            var bottomPanel = new WrapPanel()
            {
            };
            var replyesBlock = new TextBlock()
            {
                FontSize = 24,
                FontFamily = new FontFamily("Calibri Light"),
                Text = "Ответы(n): "
            };
            var showReply = new Button()
            {
                FontSize = 24,
                FontFamily = new FontFamily("Calibri Light"),
                Content = "Посмотреть"
            };
            bottomPanel.Children.Add(replyesBlock);
            bottomPanel.Children.Add(showReply);
            mainPanel.Children.Add(titleBlock);
            mainPanel.Children.Add(dateBlock);
            mainPanel.Children.Add(bottomPanel);

            content.Children.Add(mainPanel);
        }
    }
}
