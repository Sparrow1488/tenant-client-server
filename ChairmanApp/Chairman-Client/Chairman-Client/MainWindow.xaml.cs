using MVVM_Pattern_Test.ViewModels;
using System;
using System.Windows;

namespace Chairman_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var authVM = new AuthorizationVM();
            authVM.CloseAuthWindow = new Action(Close);
            DataContext = authVM;
        }
    }
}
