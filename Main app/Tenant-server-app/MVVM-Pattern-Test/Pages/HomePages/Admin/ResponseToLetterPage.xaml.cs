using MVVM_Pattern_Test.ViewModels.Admin;
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

namespace MVVM_Pattern_Test.Pages.HomePages.Admin
{
    /// <summary>
    /// Логика взаимодействия для ResponseToLetterPage.xaml
    /// </summary>
    public partial class ResponseToLetterPage : Page
    {
        public ResponseToLetterPage(int letterId)
        {
            InitializeComponent();
            DataContext = new AdminReplyerVM(letterId);
        }
    }
}
