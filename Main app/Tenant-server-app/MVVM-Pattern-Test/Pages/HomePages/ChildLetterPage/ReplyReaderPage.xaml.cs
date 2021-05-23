using ExchangeSystem.Requests.Objects.Entities;
using MVVM_Pattern_Test.ViewModels.LettersViewModels;
using System.Collections.Generic;
using System.Windows.Controls;
//using WpfApp1.MyApplication;

namespace WpfApp1.Pages.HomePages.ChildLetterPage
{
    /// <summary>
    /// Логика взаимодействия для ReplyReaderPage.xaml
    /// </summary>
    public partial class ReplyReaderPage : Page
    {
        private List<Letter> MySendLetters = new List<Letter>();
        public ReplyReaderPage(int letterId)
        {
            InitializeComponent();

            DataContext = new ResponseReaderVM(letterId);
        }
    }
}
