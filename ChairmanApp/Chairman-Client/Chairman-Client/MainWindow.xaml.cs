using MVVM_Pattern_Test.ViewModels;
using System;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Server;
using WpfApp1.Server.Packages.PersonalDir;
using WpfApp1.Server.ServerExceptions;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var authVM = new AuthorizationVM();
            authVM.CloseAuthWindow = new Action(Close);
            DataContext = authVM;
        }
    }
}
