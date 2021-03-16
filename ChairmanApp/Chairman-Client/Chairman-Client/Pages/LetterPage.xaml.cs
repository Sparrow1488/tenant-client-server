using Chairman_Client.ApplicationService;
using Chairman_Client.Server.Chairman.Functions;
using Chairman_Client.ViewModels;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для LetterPage.xaml
    /// </summary>
    public partial class LetterPage : Page
    {
        public LetterPage()
        {
            InitializeComponent();
            DataContext = new LettersVM();
        }
    }
}
