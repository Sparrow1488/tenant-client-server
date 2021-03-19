using MVVM_Pattern_Test.ViewModels;
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

namespace MVVM_Pattern_Test.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            var authVM = new AuthorizationVM();
            DataContext = authVM;
            authVM.CloseAuthWindow = new Action(this.Close);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //new HomeWindow().Show();
        }
    }
}
