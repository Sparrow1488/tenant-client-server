using MVVM_Pattern_Test.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
//using WpfApp1.MyApplication;
using WpfApp1.Pages.HomePages.ChildLetterPage;
using WpfApp1.Server.Packages.SourceDir;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для LetterPage.xaml
    /// </summary>
    public partial class LetterPage : Page
    {
        public static TextBlock ExceptionText;
        //private static ApplicationEvents applicationEvents = new ApplicationEvents();
        private Page universalPage = new UniversalLetterPage();
        private Page replyesPage = new ReplyReaderPage(null);

        public static Frame AdditionalFrame = null;
        public LetterPage()
        {
            InitializeComponent();
            DataContext = new LettersVM();

            ExceptionText = exceptionText;
            AdditionalFrame = additionalFrame;
        }

        //public static void ShowMessage(string text)
        //{
        //    //applicationEvents.ShowEventMessage(text, ExceptionText);
        //}
        //public static void ShowExceptionMessage(string text)
        //{
        //    //applicationEvents.ShowExceptionMessage(text, ExceptionText);
        //}

        //private void SelectComplaintPageBtn_Click(object sender, RoutedEventArgs e)
        //{
           
        //}

        //private void SelectOfferPageBtn_Click(object sender, RoutedEventArgs e)
        //{
           
        //}

        //private bool myLettersWasLoaded = false;
        //private async void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (myLettersWasLoaded == false)
        //    {
        //        var myLettersCollection = await JumboServer.ActiveServer.GetMyLetters();
        //        replyesPage = new ReplyReaderPage(myLettersCollection);
        //        myLettersWasLoaded = true;
        //    }
        //    frameBox.Content = replyesPage;
        //}

        //private void SelectQuestionPageBtn_Click(object sender, RoutedEventArgs e)
        //{
            
        //}

        //private void bottomPanel_MouseEnter(object sender, MouseEventArgs e)
        //{
            
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    frameBox.Content = null;
        //    frameBox.Content = universalPage;
        //}
    }
}
