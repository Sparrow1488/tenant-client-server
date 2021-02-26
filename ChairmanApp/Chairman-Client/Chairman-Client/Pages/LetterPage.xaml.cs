using Chairman_Client.Pages.LetterPageChildren;
using Chairman_Client.Server.Chairman.Functions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                MaxWidth = 250, 
                Background = new SolidColorBrush(Colors.AliceBlue),
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
            var sender = new TextBlock()
            {
                Margin = new Thickness(5, 0, 5, 0),
                FontSize = 12,
                Text = "SENDER: " + newLetter.SenderLogin,
                TextWrapping = TextWrapping.Wrap
            };
            //var readButton = new Button()
            //{
            //    Margin = new Thickness(5, 0, 5, 0),
            //    FontSize = 12,
            //    BorderBrush = new SolidColorBrush(Colors.Transparent),
            //    Background = new SolidColorBrush(Colors.Transparent),
            //    Content = "Читать"
            //};
            letterSmallPanel.Children.Add(title);
            letterSmallPanel.Children.Add(typeLetter);
            letterSmallPanel.Children.Add(sender);
            //letterSmallPanel.Children.Add(readButton);

            title.MouseDown += OpenLetter;
            return letterSmallPanel;
        }
        private void OpenLetter(object sender, RoutedEventArgs e)
        {
            var title = (TextBlock)sender;
            title.FontWeight = FontWeights.DemiBold;
            letterReaderFrame.Content = new ReadLettersPage(null); //TODO: создать класс, который будет содержать в себе панель отображения письма и само письмо
        }
        private void StackPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        private void addNewTestLetter_Click(object sender, RoutedEventArgs e)
        {
            var tenant = "asd";
            var testLetter = new ComplaintLetter("Проблемка образовалась", "Хочу помощи Хочу помощи Хочу помощи Хочу помощи", tenant);
            var newPanel = CreateNewLetterOnLeftPanel(testLetter);
            leftLettersList.Children.Add(newPanel);
        }
    }
}
