using Multi_Server_Test.Server.Packages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

namespace WpfApp1.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для NoticePage.xaml
    /// </summary>
    public partial class NoticePage : Page
    {
        private NewsCollection ReceivedNews;
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
                    ReceivedNews = await HomeWindow.Server.ReceiveNewsCollection();
                    foreach (var news in ReceivedNews.Collection)
                    {
                        AddNewsTitleAndDescription(news.Title, news.Description);
                    }
                    IsLoadNews = true;
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Возникли проблемы в ходе подключения к серверу", 
                    "Ошибка подключения",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void addPanelBtn_Click(object sender, RoutedEventArgs e)
        {
            AddNewsTitleAndDescription("Пицца", "Описание пиццы");
        }
        private void AddNewsTitleAndDescription(string title, string desc)
        {
            var mainPanel = new StackPanel()
            {
                Background = new SolidColorBrush(Color.FromRgb(238, 234, 233)),
                Orientation = Orientation.Vertical,
                Margin = new Thickness(0, 10, 0, 0)
            };
            var topBorder = new StackPanel()
            {
                Background = new SolidColorBrush(Colors.Black),
                Height = 2,
            };
            var titleBlock = new TextBlock()
            {
                FontSize = 24,
                FontWeight = FontWeights.DemiBold,
                Text = title,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(20, 5, 0, 5)
            };
            var descriptionBlock = new TextBlock()
            {
                Text = desc,
                FontSize = 20,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5, 0, 5, 5)
            };

            mainPanel.Children.Add(topBorder);
            mainPanel.Children.Add(titleBlock);
            mainPanel.Children.Add(descriptionBlock);

            //TODO: сделать показ картинок

            //if (image != null)
            //{
            //    MemoryStream ms = new MemoryStream(image);
            //    //System.Drawing.Image returnImage = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(ms);
            //    var bitmapImage = new BitmapImage();
            //    bitmapImage.BeginInit();
            //    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            //    bitmapImage.StreamSource = ms;
            //    bitmapImage.EndInit();

            //    var imagePanel = new Image()
            //    {
            //        Source = bitmapImage,
            //        Height = 400
            //    };
            //    mainPanel.Children.Add(imagePanel);
            //}
            
            contentPanel.Children.Insert(0, mainPanel);
        }
    }
}
