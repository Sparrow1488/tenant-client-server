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
        private double _animateToProperty = 105;
        private double _animateFromProperty = 0;
        private void profile_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var tabButton = sender as Button;
            tabButton.Margin = new Thickness(0, 0, -15, 0);
            //var anim = new DoubleAnimation();
            //anim.Duration = new Duration(TimeSpan.FromMilliseconds(200));
            //_animateFromProperty = tabButton.ActualWidth;
            //anim.From = _animateFromProperty;
            //anim.To = _animateToProperty;
            //tabButton.BeginAnimation(WidthProperty, anim);
        }

        private void profile_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var tabButton = sender as Button;
            tabButton.Margin = new Thickness(0);
            //var anim = new DoubleAnimation();
            //anim.Duration = new Duration(TimeSpan.FromMilliseconds(200));
            //anim.From = _animateToProperty;
            //anim.To = _animateFromProperty;
            //tabButton.BeginAnimation(WidthProperty, anim);
        }
    }
}
