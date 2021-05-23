using ExchangeSystem.Requests.Objects.Entities;
using MVVM_Pattern_Test.ViewModels;
using System.Windows.Controls;

namespace WpfApp1.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage(User user)
        {
            InitializeComponent();
            DataContext = new ProfileVM(user);
        }
    }
}
