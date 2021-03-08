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
using WpfApp1.MyApplication;
using WpfApp1.Pages.HomePages.ChildLetterPage;

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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            frameBox.Content = infoPage;
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
