using System;
using System.Windows;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace WpfApp1
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

        private static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "6CScUkKUdSLgSDtq1QWtfY2NCPP57aa6ajBn7R4Y",
            BasePath = "https://client-server-testapp-default-rtdb.firebaseio.com/"
        };
        private static FirebaseClient client = null;
        private static string parentPath = "Users";
        private static MyUser ActiveUser = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client = new FirebaseClient(config);
            if (client != null)
            {
                Console.WriteLine("Client is connected!");
            }
            else
            {
                throw new ArgumentNullException("Ошибка подключения");
            }
        }
        //client.Set($"{parentPath}/{ActiveUser.Login}", ActiveUser);
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var wasUser = client.Get($"{parentPath}/{"fifa"}");
            MyUser getUser = wasUser.ResultAs<MyUser>();
            Console.WriteLine(getUser);
        }
    }
}
