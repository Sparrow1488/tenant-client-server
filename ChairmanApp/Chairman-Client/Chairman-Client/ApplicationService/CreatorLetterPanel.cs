using Chairman_Client.Pages.LetterPageChildren;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Server.Packages.Letters;

namespace Chairman_Client.ApplicationService
{
    public class CreatorLetterPanel
    {
        public Letter ActiveLetter = null;
        public Frame parentDefaultFrame = null;
        public CreatorLetterPanel(Letter letter, Frame frameForDefaultPanel)
        {
            ActiveLetter = letter;
            parentDefaultFrame = frameForDefaultPanel;
        }
        public StackPanel CreateSmallMenuPanel()
        {
            var letterSmallPanel = new StackPanel()
            {
                MaxWidth = 280,
                Background = new SolidColorBrush(Colors.AliceBlue),
            };
            var title = new TextBlock()
            {
                Text = "→ " + ActiveLetter.Title,
                Margin = new Thickness(5),
                FontSize = 20,
                TextDecorations = TextDecorations.Underline,
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily("Calibri")
            };
            var typeLetter = new TextBlock()
            {
                Margin = new Thickness(5, 0, 5, 0),
                FontSize = 16,
                Text = "TYPE: " + ActiveLetter.LetterType,
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily("Calibri")
            };
            var sender = new TextBlock()
            {
                Margin = new Thickness(5, 0, 5, 0),
                FontSize = 16,
                Text = "SENDER: " + ActiveLetter.SenderLogin,
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily("Calibri")
            };
            letterSmallPanel.Children.Add(title);
            letterSmallPanel.Children.Add(typeLetter);
            letterSmallPanel.Children.Add(sender);

            title.MouseDown += OpenLetter;
            return letterSmallPanel;
        }
        public StackPanel CreateDefaultReadPanel()
        {
            return null;
        }
        private void OpenLetter(object sender, RoutedEventArgs e)
        {
            var title = (TextBlock)sender;
            title.FontWeight = FontWeights.DemiBold;
            parentDefaultFrame.Content = new ReadLettersPage(ActiveLetter);
        }
    }
}
