using FireSharp;
using FireSharp.Response;
using System;
using System.Threading.Tasks;
using System.Windows;
using Tenant_server_app.ServerClasses;
using System.Media;
using System.Windows.Media;
using System.Windows.Controls;

namespace Tenant_server_app
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateClinet();
        }

        public static string testLogin = "3434";
        public static string testPass = "fr56fr";
        private async void createUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //createUserBtn.IsEnabled = false;
                await AddInDb(new MyUser(testLogin, testPass));
            }
            catch (Exception ex)
            {
                ExceptionBuild(ex.Message);
            }
            finally
            {
                createUserBtn.IsEnabled = true;
            }
        }
        private async void getUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                getUserBtn.IsEnabled = false;
                await GetUser(testLogin);
            }
            catch (Exception ex)
            {
                ExceptionBuild(ex.Message);
            }
            finally
            {
                getUserBtn.IsEnabled = true;
            }
        }

        #region OTHER METHODS
        private void CreateClinet()
        {
            ServerData.serverClient = new FirebaseClient(ServerData.firebaseConfig);
            if (ServerData.serverClient != null)
            {
                testCommandsList.Items.Add("Client is connected!");
            }
            else
            {
                throw new ArgumentNullException("Ошибка подключения");
            }
        }
        private async Task AddInDb(MyUser user)
        {
            await Task.Run(() =>
            {
                ServerData.serverClient.SetAsync($"{ServerData.usersPath}/{user.Login}", user);
            });
            testCommandsList.Items.Add($"{user.Login}; {user.Password} -> is created!");
        }
        private async Task GetUser(string login)
        {
            var respose = await ServerData.serverClient.GetAsync($"{ServerData.usersPath}/{login}");
            MyUser.Active = respose.ResultAs<MyUser>();
            if (MyUser.Active == null)
            {
                throw new NullReferenceException("Не найдено ни одного совпадения!");
            }
            testCommandsList.Items.Add($"{MyUser.Active.Login}; {MyUser.Active.Password}");
        }
        #endregion

        #region TREATMENT
        private void ExceptionBuild(string exMessage)
        {
            testCommandsList.Items.Add(exMessage);
        }
        #endregion

    }
}
