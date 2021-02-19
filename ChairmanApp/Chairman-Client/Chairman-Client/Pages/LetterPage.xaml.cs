using Chairman_Client.Server.Chairman.Functions;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для LetterPage.xaml
    /// </summary>
    public partial class LetterPage : Page
    {
        private bool lettersWasLoaded = false;
        public LetterPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var getLetters = await JumboServer.ActiveServer.ReceiveLettersCollectionAsync();
            if (getLetters == null)
                MessageBox.Show("Список писем пуст");
        }
    }
}
