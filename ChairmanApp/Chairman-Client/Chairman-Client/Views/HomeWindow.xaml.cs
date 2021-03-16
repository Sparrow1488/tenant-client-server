using MVVM_Pattern_Test.ViewModels;
using System.Windows;

namespace Chairman_Client
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            DataContext = new HomeVM();
        }
    }
}
