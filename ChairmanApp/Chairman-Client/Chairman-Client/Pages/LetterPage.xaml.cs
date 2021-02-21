using Chairman_Client.Server.Chairman.Functions;
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
        private Functions serverFunctions = new Functions("secret", JumboServer.ActiveServer);
        public LetterPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (lettersWasLoaded == false)
            {
                var getLetters = await serverFunctions.GetLetters();
                if (string.IsNullOrEmpty(getLetters))
                    MessageBox.Show("Список писем пуст");
                else
                {
                    lettersWasLoaded = true;
                    MessageBox.Show(getLetters);
                }
            }
            
        }
    }
}
