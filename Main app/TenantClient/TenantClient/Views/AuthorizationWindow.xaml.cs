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
            InitializeComponent();
            AuthVm = new AuthVm();
            AuthVm.SetCloseAuthWindowAction(() => Close());
            DataContext = AuthVm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //await Task.Run(()=>
            //{
            //    AuthVm.LogInToken.Execute(null); //TODO: давайте не будем говнить MVVM, просто скачаем библу
            //});
        }
    }
}
