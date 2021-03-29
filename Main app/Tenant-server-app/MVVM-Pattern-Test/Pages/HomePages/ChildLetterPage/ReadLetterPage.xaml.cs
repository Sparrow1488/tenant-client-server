using Microsoft.Win32;
using MVVM_Pattern_Test.ViewModels.LettersViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using WpfApp1.Server.Packages.Letters;

namespace MVVM_Pattern_Test.Pages.HomePages.ChildLetterPage
{
    /// <summary>
    /// Логика взаимодействия для ReadLetterPage.xaml
    /// </summary>
    public partial class ReadLetterPage : Page
    {
        public ReadLetterPage(Letter readLetter)
        {
            InitializeComponent();
            DataContext = new ReadLetterVM(readLetter); 
        }

        
    }
}
