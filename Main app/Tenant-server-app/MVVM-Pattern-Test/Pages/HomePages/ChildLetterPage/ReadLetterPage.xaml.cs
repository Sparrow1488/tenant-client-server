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

        private double firstlyWidth = 120;
        private void attachedImage_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var image = (Image)sender;
            firstlyWidth = image.Width;

            var anim = new DoubleAnimation();
            anim.From = firstlyWidth;
            anim.To = firstlyWidth * 2;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(200));
            image.BeginAnimation(WidthProperty, anim);
        }

        private void attachedImage_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var image = (Image)sender;

            var anim = new DoubleAnimation();
            anim.From = image.Width;
            anim.To = firstlyWidth;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(200));
            image.BeginAnimation(WidthProperty, anim);
        }

        private void attachedImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var result = MessageBox.Show("Желаете сохранить картинку?", "Работа со вложениями", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Типо сохранилась");
            }
        }
    }
}
