using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.MyApplication;
using WpfApp1.Pages.HomePages.ChildLetterPage;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для LetterPage.xaml
    /// </summary>
    public partial class LetterPage : Page
    {
        public static TextBlock ExceptionText;
        private static ApplicationEvents applicationEvents = new ApplicationEvents();

        private Page complaintPage = new ComplaintPage();
        private Page offerPage = new OfferPage();
        private Page questionPage = new QuestionPage();
        private Page infoPage = new InformationLetterPage();
        private Page universalPage = new UniversalLetterPage();
        private Page replyesPage = new ReplyReaderPage(null);
        public LetterPage()
        {
            InitializeComponent();
            ExceptionText = exceptionText;
        }

        public static void ShowMessage(string text)
        {
            applicationEvents.ShowEventMessage(text, ExceptionText);
        }
        public static void ShowExceptionMessage(string text)
        {
            applicationEvents.ShowExceptionMessage(text, ExceptionText);
        }

        private void SelectComplaintPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frameBox.Content = null;
            frameBox.Content = complaintPage;
        }

        private void SelectOfferPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frameBox.Content = null;
            frameBox.Content = offerPage;
        }

        private bool myLettersWasLoaded = false;
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (myLettersWasLoaded == false)
            {
                var myLettersCollection = await JumboServer.ActiveServer.GetMyLetters();
                replyesPage = new ReplyReaderPage(myLettersCollection);
                myLettersWasLoaded = true;
            }
            frameBox.Content = replyesPage;
        }

        private void SelectQuestionPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frameBox.Content = null;
            frameBox.Content = questionPage;
        }

        private void bottomPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frameBox.Content = null;
            frameBox.Content = universalPage;
        }
    }
}
