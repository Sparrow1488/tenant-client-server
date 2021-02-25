using Chairman_Client.Server.Chairman.Functions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Server;
using WpfApp1.Server.Packages.Letters;
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
                if (getLetters == null)
                    MessageBox.Show("Список писем пуст");
                else
                {
                    lettersWasLoaded = true;
                    foreach (var letter in getLetters)
                    {
                        var panel = CreateNewLetterOnLeftPanel(letter);
                        leftLettersList.Children.Add(panel);
                    }
                }
            }
        }
        private StackPanel CreateNewLetterOnLeftPanel(Letter newLetter)
        {
            var letterSmallPanel = new StackPanel() 
            { 
                MaxWidth = 120, 
                Background = new SolidColorBrush(Colors.AliceBlue),
                //добавить MouseDown
            };
            var title = new TextBlock()
            {
                Text = "→ " + newLetter.Title,
                Margin = new Thickness(5),
                FontSize = 15,
                TextDecorations = TextDecorations.Underline,
                TextWrapping = TextWrapping.Wrap
            };
            var typeLetter = new TextBlock()
            {
                Margin = new Thickness(5, 0, 5, 0),
                FontSize = 12,
                Text = "TYPE: " + newLetter.LetterType,
                TextWrapping = TextWrapping.Wrap
            };
            var descLetter = new TextBlock()
            {
                Margin = new Thickness(5, 0, 5, 0),
                FontSize = 12,
                Text = "DESC: " + newLetter.Description,
                TextWrapping = TextWrapping.Wrap
            };

            letterSmallPanel.Children.Add(title);
            letterSmallPanel.Children.Add(typeLetter);
            letterSmallPanel.Children.Add(descLetter);
            return letterSmallPanel;
        }

        private void StackPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void addNewTestLetter_Click(object sender, RoutedEventArgs e)
        {
            var tenant = new Person("asd", "1234", 77);
            var testLetter = new ComplaintLetter("Проблемка образовалась", "Хочу помощи Хочу помощи Хочу помощи Хочу помощи", tenant);
            var newPanel = CreateNewLetterOnLeftPanel(testLetter);
            leftLettersList.Children.Add(newPanel);
        }
    }
}
