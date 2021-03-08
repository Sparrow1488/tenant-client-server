using Chairman_Client.ApplicationService;
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (lettersWasLoaded == false)
            {
                LoadLetters();
                lettersWasLoaded = true;
            }
        }
        private async void LoadLetters()
        {
            var getLetters = await serverFunctions.GetLetters();
            if (getLetters == null)
                MessageBox.Show("Список писем пуст");
            else
            {
                lettersWasLoaded = true;
                foreach (var letter in getLetters)
                {
                    var panel = new CreatorLetterPanel(letter, letterReaderFrame).CreateSmallMenuPanel();
                    leftLettersList.Children.Add(panel);
                }
            }
        }
        private void ReloadBtn_Click(object sender, RoutedEventArgs e)
        {
            leftLettersList.Children.Clear();
            LoadLetters();
        }
    }
}
