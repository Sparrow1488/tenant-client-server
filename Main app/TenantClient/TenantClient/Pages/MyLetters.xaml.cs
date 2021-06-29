using System;
using System.Windows;
using System.Windows.Controls;
using TenantClient.ViewModels;

namespace TenantClient.Pages
{
    /// <summary>
    /// Логика взаимодействия для MyLetters.xaml
    /// </summary>
    public partial class MyLetters : Page
    {
        public MyLetters()
        {
            InitializeComponent();
            DataContext = new MyLettersVm();
        }
    }
}
