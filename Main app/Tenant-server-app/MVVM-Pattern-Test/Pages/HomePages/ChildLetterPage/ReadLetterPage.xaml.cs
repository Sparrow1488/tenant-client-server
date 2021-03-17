using MVVM_Pattern_Test.ViewModels.LettersViewModels;
using System.Windows.Controls;
using WpfApp1.Server.Packages.Letters;

namespace MVVM_Pattern_Test.Pages.HomePages.ChildLetterPage
{
    /// <summary>
    /// Логика взаимодействия для ReadLetterPage.xaml
    /// </summary>
    public partial class ReadLetterPage : Page
    {
        
        public ReadLetterPage(Letter readLetter)
        {
            InitializeComponent();
            DataContext = new ReadLetterVM(readLetter); 
        }
    }
}
