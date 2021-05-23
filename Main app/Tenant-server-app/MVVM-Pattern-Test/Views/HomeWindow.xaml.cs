using ExchangeSystem.Requests.Objects.Entities;
using MVVM_Pattern_Test.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace MVVM_Pattern_Test.Views
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow(User user)
        {
            InitializeComponent();
            DataContext = new HomeVM(user);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var authUser = JumboServer.ActiveServer.ActiveUser;
            //if (authUser.AdminStatus != 1)
            //{
            //    viewAllLettersBtn.Visibility = Visibility.Collapsed;
            //    viewWriteNewsBtn.Visibility = Visibility.Collapsed;
            //}
        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ProfilePageFrame.Opacity = 0.6;
            ProfilePageFrame.IsEnabled = false;
            var anim = new DoubleAnimation();
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(200));
            anim.From = 50;
            anim.To = 250;
            menuLeftPanel.BeginAnimation(WidthProperty, anim);
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ProfilePageFrame.Opacity = 1;
            ProfilePageFrame.IsEnabled = true;
            var anim = new DoubleAnimation();
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(200));
            anim.From = 250;
            anim.To = 50;
            menuLeftPanel.BeginAnimation(WidthProperty, anim);
        }
        private void EditSenderName(string newName, object sender)
        {
            var button = (ToggleButton)sender;
            button.Content = newName;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            string tokenPath = "./token-auth.txt";
            if (File.Exists(tokenPath))
            {
                File.Delete("./token-auth.txt");
            }
            Close();
        }
    }
}
