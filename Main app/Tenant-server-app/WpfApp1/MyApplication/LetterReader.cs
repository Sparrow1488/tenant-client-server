using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Pages.HomePages.ChildLetterPage;
using WpfApp1.Server.Packages.Letters;

namespace WpfApp1.MyApplication
{
    public class LetterReader
    {
        private Letter ActiveLetter = null;
        private Frame ContentFrame = null;
        public LetterReader(Letter readLetter, Frame frameForPage)
        {
            ActiveLetter = readLetter;
            ContentFrame = frameForPage;
        }
        public void AddMyLetterOnPanel(StackPanel contentPanel)
        {
            var mainPanel = new StackPanel()
            {
                Margin = new Thickness(15, 2, 15, 2),
            };
            var titleBlock = new TextBlock()
            {
                FontSize = 24,
                FontFamily = new FontFamily("Calibri Light"),
                Text = "Заголовок: " + ActiveLetter.Title
            };
            var dateBlock = new TextBlock()
            {
                FontSize = 24,
                FontFamily = new FontFamily("Calibri Light"),
                Text = "Дата отправки: " + ActiveLetter.DateCreate.ToShortDateString()
            };
            int countSources = 0;
            if (ActiveLetter.SourcesTokens != null)
            {
                for (int i = 0; i < ActiveLetter.SourcesTokens.Length; i++)
                {
                    if (ActiveLetter.SourcesTokens[i] != null)
                        countSources++;
                }
            }
            var countSourcesBlock = new TextBlock()
            {
                FontSize = 24,
                FontFamily = new FontFamily("Calibri Light"),
                Text = "Вложения: " + countSources
            };
            var bottomPanel = new WrapPanel()
            {
            };
            var replyesBlock = new TextBlock()
            {
                FontSize = 24,
                FontFamily = new FontFamily("Calibri Light"),
                Text = "Ответы(n): "
            };
            var showReply = new Button()
            {
                FontSize = 24,
                FontFamily = new FontFamily("Calibri Light"),
                Content = "Посмотреть"
            };
            bottomPanel.Children.Add(replyesBlock);
            bottomPanel.Children.Add(showReply);
            mainPanel.Children.Add(titleBlock);
            mainPanel.Children.Add(dateBlock);
            mainPanel.Children.Add(countSourcesBlock);
            mainPanel.Children.Add(bottomPanel);
            showReply.Click += ReadLetter;

            contentPanel.Children.Add(mainPanel);
        }

        private void ReadLetter(object sender, RoutedEventArgs e)
        {
            ContentFrame.Visibility = Visibility.Visible;
            ContentFrame.Content = new UniversalLetterPage(ActiveLetter);
        }
    }
}
