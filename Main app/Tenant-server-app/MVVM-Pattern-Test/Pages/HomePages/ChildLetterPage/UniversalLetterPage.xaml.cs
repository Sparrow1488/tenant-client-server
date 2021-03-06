﻿using MVVM_Pattern_Test.ViewModels.LettersViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp1.Server.Packages.Letters;
using System.Collections.ObjectModel;

namespace WpfApp1.Pages.HomePages.ChildLetterPage
{
    /// <summary>
    /// Логика взаимодействия для UniversalLetterPage.xaml
    /// </summary>
    public partial class UniversalLetterPage : Page
    {
        private ObservableCollection<string> UploadedSourceTokens = new ObservableCollection<string>();
        public UniversalLetterPage()
        {
            InitializeComponent();

            DataContext = new LetterSenderVM();
        }
        public UniversalLetterPage(Letter readLetter)
        {
            InitializeComponent();

            titleBox.Text = readLetter.Title;
            descBox.Text = readLetter.Description;

            sendBtn.IsEnabled = false;
            sourceAttacherBtn.IsEnabled = false;
            if(readLetter.SourcesTokens != null)
            {
                for (int i = 0; i < readLetter.SourcesTokens.Length; i++)
                    if(readLetter.SourcesTokens[i] != null)
                        AddInAttechedList(readLetter.SourcesTokens[i]);
            }
                
        }

        private async void SendLetterBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void descBox_MouseEnter(object sender, MouseEventArgs e)
        {
            var textBox = (Border)sender;
            textBox.BorderThickness = new Thickness(1, 0, 1, 0);
            textBox.BorderBrush = new SolidColorBrush(Colors.Purple);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            var textBox = (Border)sender;
            textBox.BorderThickness = new Thickness(0);
        }

        private async void AttachFile_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddInAttechedList(string token)
        {
            sourceAtteched.Visibility = Visibility.Visible;
            UploadedSourceTokens.Add(token);
            sourceAtteched.Items.Add("Source token: " + token);
        }
    }
}
