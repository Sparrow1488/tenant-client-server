using Multi_Server_Test.Server;
using WpfApp1.Server.ServerMeta;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using WpfApp1.Server.ServerExceptions;
using MVVM_Pattern_Test.ViewModels.NewsViewModels;

namespace WpfApp1.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для NoticePage.xaml
    /// </summary>
    public partial class NoticePage : Page
    {
        public NoticePage()
        {
            InitializeComponent();
            DataContext = new RecieveNewsVM();
        }
    }
}
