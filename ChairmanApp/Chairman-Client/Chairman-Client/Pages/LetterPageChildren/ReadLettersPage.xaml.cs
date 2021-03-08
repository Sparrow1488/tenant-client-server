using Chairman_Client.ApplicationService;
using Chairman_Client.Server.Chairman.Functions;
using Chairman_Client.Server.Packages.LettersDir;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client.Pages.LetterPageChildren
{
    /// <summary>
    /// Логика взаимодействия для ReadLettersPage.xaml
    /// </summary>
    public partial class ReadLettersPage : Page
    {
        private Letter ReadLetter = null;
        private Functions serverFunctions = new Functions("secret", JumboServer.ActiveServer);
        public ReadLettersPage(Letter readLetter)
        {
            InitializeComponent();
            ReadLetter = readLetter;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            topTitle.Text = ReadLetter.Title;
            typeLetter.Text = "Тип письма: " + ReadLetter.LetterType;
            senderLetter.Text = "Отправитель (логин): " +  ReadLetter.SenderLogin;
            dateSend.Text = $"Дата создания: {ReadLetter.DateCreate.Day}/{ReadLetter.DateCreate.Month}/{ReadLetter.DateCreate.Year}";

            mainTitle.Text = ReadLetter.Title;
            descLetter.Text = ReadLetter.Description;
        }

        private async void replyBtn_Click(object sender, RoutedEventArgs e)
        {
            responseFrame.Content = new ResponseToLetterPage(ReadLetter);
        }
    }
}
