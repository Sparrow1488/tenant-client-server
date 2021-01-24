using FireSharp;
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
        public delegate void ConnectDelegate();
        public event ConnectDelegate ConnectStatusEvent;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ConnectStatusEvent += ConnectStatusBuild;
                CreateClinet();
            }
            catch(Exception ex)
            {
                ExceptionBuild(ex.Message);
            }
            finally
            {
                ConnectStatusEvent?.Invoke();
            }
        }

        public static string testLogin = "3434";
        public static string testPass = "fr56fr";
        private async void createUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                createUserBtn.IsEnabled = false;
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
                var getUser = await GetUser(testLogin);
                testCommandsList.Items.Add($"Login: {getUser.Login}; Password: {getUser.Password}");
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
                testCommandsList.Items.Add("FireBase client started work");
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
            testCommandsList.Items.Add($"{user.Login}; {user.Password} → is created!");
        }
        private async Task<MyUser> GetUser(string login)
        {
            var respose = await ServerData.serverClient.GetAsync($"{ServerData.usersPath}/{login}");
            var user = respose.ResultAs<MyUser>();
            if (user == null)
            {
                throw new NullReferenceException("Не найдено ни одного совпадения!");
            }
            return user;
        }
        #endregion

        #region TREATMENT
        private void ExceptionBuild(string exMessage)
        {
            testCommandsList.Items.Add("Обработано исключение:" + exMessage);
        }
        private void ConnectStatusBuild()
        {
            if (ServerData.serverClient != null)
            {
                connectStatus_TB.Text = "Connected";
                connectStatus_TB.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                connectStatus_TB.Text = "Not connected";
                connectStatus_TB.Foreground = new SolidColorBrush(Colors.DarkOrange);
            }
        }
        #endregion

    }
}
