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
using WpfApp1.Server.Packages.Letters;

namespace Chairman_Client.Pages.LetterPageChildren
{
    /// <summary>
    /// Логика взаимодействия для ReadLettersPage.xaml
    /// </summary>
    public partial class ReadLettersPage : Page
    {
        private static Letter ReadLetter = null;
        public ReadLettersPage(Letter readLetter)
        {
            InitializeComponent();
            ReadLetter = readLetter;
        }
        public static void ShowPage(Letter letter)
        {
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(ReadLetter.Description, ReadLetter.Title);
        }
    }
}
