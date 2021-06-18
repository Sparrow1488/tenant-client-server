using System.Windows;
using TenantClient.ViewModels;

namespace TenantClient
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private AuthVm AuthVm;
        public AuthorizationWindow()
        {
            //using System.Windows.Interactivity;
            InitializeComponent();
            AuthVm = new AuthVm();
            AuthVm.SetCloseAuthWindowAction(() => Close());
            DataContext = AuthVm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AuthVm.LogInToken.Execute(null); //TODO: давайте не будем говнить MVVM, просто скачаем библу
        }
    }
}
