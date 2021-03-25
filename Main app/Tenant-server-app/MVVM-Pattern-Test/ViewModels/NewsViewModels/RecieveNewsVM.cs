using Multi_Server_Test.Server;
using MVVM_Pattern_Test.Commands;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.ViewModels.NewsViewModels
{
    public class RecieveNewsVM : BaseVM
    {
        public RecieveNewsVM()
        {
            RecieveNews.Execute(null);
        }
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; OnPropertyChanged(); } }
        public List<News> RecievedNews
        { 
            get { return _recievedNews; } 
            set { _recievedNews = value; OnPropertyChanged(); } 
        }
        private List<News> _recievedNews = new List<News>();
        public List<Border> NewsPanels
        {
            get { return _newsPanels; }
            set { _newsPanels = value; OnPropertyChanged(); }
        }
        private List<Border> _newsPanels = new List<Border>();

        #region Commands
        public MyCommand RecieveNews
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    var getNews = await JumboServer.ActiveServer.ReceiveNewsCollectionAsync();
                    Notice = "Загрузка новостей...";
                    if (getNews != null)
                    {
                        RecievedNews = getNews;
                        foreach (var news in RecievedNews)
                        {
                            var newContentPanel = AddNewsPanel(news);
                            NewsPanels.Add(newContentPanel);
                        }
                        Notice = "Новости загружены";
                    }
                });
            }
        }
        #endregion

        #region Methods
        private Border AddNewsPanel(News news)
        {
            var mainBorder = new Border()
            {
                BorderBrush = new SolidColorBrush(Colors.Gray),
                BorderThickness = new Thickness(1),
                Padding = new Thickness(5, 10, 5, 10),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(0, 0, 0, 5)
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

            return mainBorder;
        }
        #endregion
    }
}
