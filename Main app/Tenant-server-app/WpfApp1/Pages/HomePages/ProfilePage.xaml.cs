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
using WpfApp1.Classes;

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
        }

        private bool InfoUserIsLoaded = false;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO: ошибка nullRefException
            loginInfo.Text += HomeWindow.Server.ActiveUser.Login;
            nameInfo.Text += HomeWindow.Server.ActiveUser.Name;
            lastNameInfo.Text += HomeWindow.Server.ActiveUser.LastName;

            InfoUserIsLoaded = true;
        }
    }
}
