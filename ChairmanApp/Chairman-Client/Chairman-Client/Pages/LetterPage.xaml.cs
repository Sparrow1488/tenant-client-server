using Chairman_Client.Server.Chairman.Functions;
using ChairmanClient.Server.ServerMeta;
using System.Windows;
using System.Windows.Controls;

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
            await Functions.Active.GetLetters();
        }
    }
}
