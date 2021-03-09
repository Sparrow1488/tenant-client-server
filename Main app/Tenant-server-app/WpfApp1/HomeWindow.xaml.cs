using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Pages.HomePages;
using WpfApp1.Server;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private Page profilePage;
        private Page noticePage;
        private Page letterPage;

        public Person GetActiveUserInfo()
        {
            return JumboServer.ActiveServer.ActiveUser;
        }

        public HomeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            profilePage = new ProfilePage();
            noticePage = new NoticePage();
            letterPage = new LetterPage();

        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfilePageFrame.Content = profilePage;
        }


        private void NoticeBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfilePageFrame.Content = noticePage;
        }

        private void LetterBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfilePageFrame.Content = letterPage;
        }

        private void menuLeftPanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ProfilePageFrame.Opacity = 0.4;
        }

        private void menuLeftPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ProfilePageFrame.Opacity = 1;
        }
    }
}
