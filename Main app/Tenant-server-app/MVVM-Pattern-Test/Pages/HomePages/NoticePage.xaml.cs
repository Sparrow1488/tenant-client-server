using MVVM_Pattern_Test.ViewModels.NewsViewModels;
using System.Windows.Controls;

namespace WpfApp1.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для NoticePage.xaml
    /// </summary>
    public partial class NoticePage : Page
    {
        public NoticePage()
        {
            InitializeComponent();
            DataContext = new RecieveNewsVM();
        }
    }
}
