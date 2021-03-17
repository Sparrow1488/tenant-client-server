using MVVM_Pattern_Test.ViewModels.Admin;
using System.Windows.Controls;

namespace MVVM_Pattern_Test.Pages.HomePages.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminReaderPage.xaml
    /// </summary>
    public partial class AdminReaderPage : Page
    {
        public AdminReaderPage()
        {
            InitializeComponent();
            DataContext = new AdminLettersVM();
        }
    }
}
