﻿using System;
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
using WpfApp1.Pages.HomePages.ChildLetterPage;

namespace WpfApp1.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для LetterPage.xaml
    /// </summary>
    public partial class LetterPage : Page
    {
        private Page complaintPage = new ComplaintPage();
        public LetterPage()
        {
            InitializeComponent();
        }

        private void complaintBtn_Click(object sender, RoutedEventArgs e)
        {
            frameBox.Content = null;
            frameBox.Content = complaintPage;
        }
    }
}
