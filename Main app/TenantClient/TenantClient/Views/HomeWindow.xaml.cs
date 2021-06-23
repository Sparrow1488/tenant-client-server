using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using TenantClient.ViewModels;

namespace TenantClient.Views
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            DataContext = new MainVm();
        }
        
        private void profile_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var tabButton = sender as Button;
            PlayMarginAnimation(tabButton, 200, -15);
        }

        private void profile_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var tabButton = sender as Button;
            PlayMarginAnimation(tabButton, 200, 0);
        }

        private void PlayMarginAnimation(Button sendlerBtn, double millisDuration, double animTo)
        {
            var anim = new ThicknessAnimation();
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(millisDuration));
            anim.From = sendlerBtn.BorderThickness;
            anim.To = new Thickness(0, 0, animTo, 0);
            sendlerBtn.BeginAnimation(MarginProperty, anim);
        }
    }
}
