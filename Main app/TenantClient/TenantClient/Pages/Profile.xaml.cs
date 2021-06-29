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
using TenantClient.ViewModels;

namespace TenantClient.Pages
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        
        private ProfileVm ProfileVm;
        public Profile()
        {
            InitializeComponent();
            ProfileVm = new ProfileVm();
            DataContext = ProfileVm;
        }

        bool accWasLoaded = false;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(!accWasLoaded)
                ProfileVm.ShowAccount.Execute(null);
            accWasLoaded = true;
        }
    }
}
