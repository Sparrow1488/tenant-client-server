using System.Windows.Controls;
using MVVM_Pattern_Test.ViewModels.LettersViewModels;
namespace WpfApp1.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для LetterPage.xaml
    /// </summary>
    public partial class LetterPage : Page
    {
        public LetterPage()
        {
            InitializeComponent();
            DataContext = new LettersVM();
        }
    }
}
