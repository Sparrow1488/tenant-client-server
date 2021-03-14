using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Server;
using WpfApp1.Server.ServerExceptions;
using WpfApp1.Server.ServerMeta;

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
            if (InfoUserIsLoaded == false)
            {
                try
                {
                    ShowUserProfileInfo(JumboServer.ActiveServer.ActiveUser);
                    InfoUserIsLoaded = true;
                }
                catch (UserNotExist ex)
                {
                    MessageBox.Show(
                        ex.Message.ToString(),
                        "Ошибка авторизации", 
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    Environment.Exit(1);
                }
            }
        }

        private void ShowUserProfileInfo(Person user)
        {
            if (user != null)
            {
                var login = user.Login;
                var name = user.Name;
                var lastName = user.LastName;
                var parentName = user.ParentName;
                var roomNumber = user.Room;

                loginInfo.Text = "Логин: " + login;
                fullNameInfo.Text = "ФИО: " + $"{lastName} {name} {parentName}";
                roomNumInfo.Text = "Квартира: " + roomNumber;
                phoneNumberInfo.Text = "Номер телефона: " + "свойства не существует";
            }
            else
                throw new UserNotExist("Данные пользователя не могут быть отображены: возможно ошибка авторизации");
        }
    }
}
