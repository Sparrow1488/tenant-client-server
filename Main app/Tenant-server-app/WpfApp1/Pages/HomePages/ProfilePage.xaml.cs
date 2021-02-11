using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
            if (InfoUserIsLoaded == false)
            {
                try
                {
                    ShowUserProfileInfo(HomeWindow.Server.ActiveUser);
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show(
                        "Ошибка авторизации: данный пользователь не может быть отображен",
                        "Authorization exception", 
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    Environment.Exit(1);
                }

                InfoUserIsLoaded = true;
            }
        }

        private void ShowUserProfileInfo(Person user)
        {
            var login = user.Login;
            var name = user.Name;
            var lastName = user.LastName;
            var parentName = user.ParentName;
            var roomNumber = user.Room;

            loginInfo.Text += login;
            fullNameInfo.Text += $"{lastName} {name} {parentName}";
            roomNumInfo.Text += roomNumber;
            phoneNumberInfo.Text += "-";
        }
    }
}
