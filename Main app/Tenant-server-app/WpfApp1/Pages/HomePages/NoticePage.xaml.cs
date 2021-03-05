using Multi_Server_Test.Server;
using WpfApp1.Server.ServerMeta;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using WpfApp1.Server.ServerExceptions;

namespace WpfApp1.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для NoticePage.xaml
    /// </summary>
    public partial class NoticePage : Page
    {
        private List<News> ReceivedNews;
        public NoticePage()
        {
            InitializeComponent();
        }

        private bool IsLoadNews = false;
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsLoadNews)
                {
                    ReceivedNews = await JumboServer.ActiveServer.ReceiveNewsCollectionAsync();
                    foreach (var news in ReceivedNews)
                    {
                        AddNewsPanel(news);
                    }
                    IsLoadNews = true;
                }
            }
            catch (JumboServerException ex)
            {
                MessageBox.Show(ex.Message.ToString(),
                                "Внутреннее исключение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }


        private void AddPanelBtn_Click(object sender, RoutedEventArgs e)
        {
            var testNews = new News("Пицца", "Описание пиццы", JumboServer.ActiveServer.ActiveUser.Login);
            AddNewsPanel(testNews);
        }
        private void AddNewsPanel(News news)
        {
            var mainBorder = new Border()
            {
                BorderBrush = new SolidColorBrush(Colors.Gray),
                BorderThickness = new Thickness(1),
                Padding = new Thickness(5, 10, 5, 10),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(0, 0, 0 ,5)
            };
            var mainPanel = new StackPanel()
            {
                //Background = new SolidColorBrush(Color.FromRgb(238, 234, 233)),
                Orientation = Orientation.Vertical,
                Margin = new Thickness(0, 10, 0, 0)
            };
            mainBorder.Child = mainPanel;
            var infoPanel = new StackPanel()
            {
                Margin = new Thickness(10, 0, 0, 0)
            };
            var typeNews = new TextBlock()
            {
                Text = "Type: " + news.Type,
                FontSize = 18,
                FontStyle = FontStyles.Italic
            };
            var authorNews = new TextBlock()
            {
                Text = "Author: " + news.Sender,
                FontSize = 18,
                FontStyle = FontStyles.Italic
            };
            var dateCreateNews = new TextBlock()
            {
                Text = "Date create: " + $"{news.DateTime.Day}/{news.DateTime.Month}/{news.DateTime.Year}",
                FontSize = 18,
                FontStyle = FontStyles.Italic
            };
            infoPanel.Children.Add(typeNews);
            infoPanel.Children.Add(authorNews);
            infoPanel.Children.Add(dateCreateNews);
            var topBorder = new StackPanel()
            {
                Background = new SolidColorBrush(Colors.DarkGray),
                Height = 1,
                Margin = new Thickness(5, 0, 5, 0)
            };
            var titleBlock = new TextBlock()
            {
                FontSize = 24,
                FontWeight = FontWeights.DemiBold,
                Text = news.Title,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(20, 5, 0, 5)
            };
            var descriptionBlock = new TextBlock()
            {
                Text = news.Description,
                FontSize = 20,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5, 0, 5, 5)
            };

            mainPanel.Children.Add(titleBlock);
            mainPanel.Children.Add(infoPanel);
            mainPanel.Children.Add(topBorder);
            mainPanel.Children.Add(descriptionBlock);

            contentPanel.Children.Add(mainBorder);
            //contentPanel.Children.Insert(0, mainPanel);
        }
    }
}
