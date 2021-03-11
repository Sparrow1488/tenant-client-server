using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM_Pattern_Test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
            
        }
        private void EditCaliber_Click(object sender, RoutedEventArgs e)
        {
            Gun gun = (Gun)Resources["gunInstance"];
            gun.Caliber = ".223";
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
