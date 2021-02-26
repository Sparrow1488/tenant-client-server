﻿using Chairman_Client.ApplicationService;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Server.Packages.Letters;

namespace Chairman_Client.Pages.LetterPageChildren
{
    /// <summary>
    /// Логика взаимодействия для ReadLettersPage.xaml
    /// </summary>
    public partial class ReadLettersPage : Page
    {
        private Letter ReadLetter = null;
        public ReadLettersPage(Letter readLetter)
        {
            InitializeComponent();
            ReadLetter = readLetter;
        }
        public void ShowDefaultPanel()
        {
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            topTitle.Text = ReadLetter.Title;
            typeLetter.Text = ReadLetter.LetterType;
            senderLetter.Text = ReadLetter.SenderLogin;

            mainTitle.Text = ReadLetter.Title;
            descLetter.Text = ReadLetter.Description;
        }
    }
}
