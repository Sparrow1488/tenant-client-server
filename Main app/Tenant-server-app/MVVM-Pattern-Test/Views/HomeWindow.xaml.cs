using MVVM_Pattern_Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.Views
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            DataContext = new HomeVM();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var authUser = JumboServer.ActiveServer.ActiveUser;
            if (authUser.AdminStatus != 1)
            {
                viewAllLettersBtn.Visibility = Visibility.Collapsed;
                viewWriteNewsBtn.Visibility = Visibility.Collapsed;
            }
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
    }
}
