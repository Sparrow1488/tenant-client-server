using MVVM_Pattern_Test.ViewModels;
using System.Windows.Controls;

namespace WpfApp1.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            DataContext = new ProfileVM();
        }
    }
}
