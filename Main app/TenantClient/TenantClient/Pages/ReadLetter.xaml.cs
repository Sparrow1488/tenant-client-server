using ExchangeSystem.v2.Entities;
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
using TenantClient.ViewModels;

namespace TenantClient.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReadLetter.xaml
    /// </summary>
    public partial class ReadLetter : Page
    {
        private ReadLetter()
        {
            InitializeComponent();
        }
        public ReadLetter(int letterId) : this()
        {
            DataContext = new ReadLetterVm(letterId);
        }
        public ReadLetter(Letter letter) : this()
        {
            DataContext = new ReadLetterVm(letter);
        }
    }
}
