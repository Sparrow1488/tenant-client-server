using MVVM_Pattern_Test.ViewModels;
using System;
using System.Windows;

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
    }
}
