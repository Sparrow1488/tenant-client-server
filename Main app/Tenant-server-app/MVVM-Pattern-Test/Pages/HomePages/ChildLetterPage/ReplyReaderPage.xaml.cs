using MVVM_Pattern_Test.ViewModels.LettersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using WpfApp1.MyApplication;
using WpfApp1.Server.Packages.Letters;

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
