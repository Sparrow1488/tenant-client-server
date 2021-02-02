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
using System.Windows.Shapes;
using WpfApp1.Pages.HomePages;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private Page profilePage;

        public HomeWindow()
        {
            InitializeComponent();
        }

        private void profileBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfilePageFrame.Content = profilePage;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            profilePage = new ProfilePage();
        }
    }
}
