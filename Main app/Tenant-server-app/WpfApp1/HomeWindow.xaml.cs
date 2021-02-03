using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;
using WpfApp1.Pages.HomePages;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public Page profilePage;
        private Page noticePage;
        public static JumboServer Server;

        public Person GetActiveUserInfo()
        {
            Console.WriteLine(Server);
            return Server.ActiveUser;
        }

        public HomeWindow()
        {
            InitializeComponent();
        }

        private void profileBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfilePageFrame.Content = null;
            ProfilePageFrame.Content = profilePage;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            profilePage = new ProfilePage();
            noticePage = new NoticePage();
        }

        private void noticeBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfilePageFrame.Content = null;
            ProfilePageFrame.Content = noticePage;
        }
    }
}
