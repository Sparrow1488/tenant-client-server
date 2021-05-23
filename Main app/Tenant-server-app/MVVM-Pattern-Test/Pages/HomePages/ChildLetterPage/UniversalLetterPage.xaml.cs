using ExchangeSystem.Requests.Objects.Entities;
using MVVM_Pattern_Test.ViewModels.LettersViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp1.Pages.HomePages.ChildLetterPage
{
    /// <summary>
    /// Логика взаимодействия для UniversalLetterPage.xaml
    /// </summary>
    public partial class UniversalLetterPage : Page
    {
        private ObservableCollection<string> UploadedSourceTokens = new ObservableCollection<string>();
        public UniversalLetterPage()
        {
            InitializeComponent();

            DataContext = new LetterSenderVM();
        }
        public UniversalLetterPage(Letter readLetter)
        {
            InitializeComponent();

            titleBox.Text = readLetter.Title;
            descBox.Text = readLetter.Text;

            //sendBtn.IsEnabled = false;
            //sourceAttacherBtn.IsEnabled = false;
            //if(readLetter.Sources != null)
            //{
            //    for (int i = 0; i < readLetter.Sources.Count; i++)
            //        if(readLetter.Sources[i] != null)
            //            AddInAttechedList(readLetter.SourcesTokens[i]);
            //}
                
        }

        private async void SendLetterBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void descBox_MouseEnter(object sender, MouseEventArgs e)
        {
            var textBox = (Border)sender;
            textBox.BorderThickness = new Thickness(1, 0, 1, 0);
            textBox.BorderBrush = new SolidColorBrush(Colors.Purple);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            var textBox = (Border)sender;
            textBox.BorderThickness = new Thickness(0);
        }

        private async void AttachFile_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddInAttechedList(string token)
        {
            sourceAtteched.Visibility = Visibility.Visible;
            UploadedSourceTokens.Add(token);
            sourceAtteched.Items.Add("Source token: " + token);
        }
    }
}
