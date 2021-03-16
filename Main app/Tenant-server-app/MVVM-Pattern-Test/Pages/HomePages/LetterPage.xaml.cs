using MVVM_Pattern_Test.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
//using WpfApp1.MyApplication;
using WpfApp1.Pages.HomePages.ChildLetterPage;
using WpfApp1.Server.Packages.SourceDir;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Pages.HomePages
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
