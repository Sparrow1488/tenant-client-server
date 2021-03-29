using Microsoft.Win32;
using MVVM_Pattern_Test.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using WpfApp1.Server.Packages.Letters;

namespace MVVM_Pattern_Test.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для AttachmentsPage.xaml
    /// </summary>
    public partial class AttachmentsPage : Page
    {
        public AttachmentsPage(Letter sourceFromLetter)
        {
            InitializeComponent();

            DataContext = new AttachmentsVM(sourceFromLetter);
        }
        private double firstlyWidth = 480;
        private void attachedImage_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var image = (Image)sender;
            if (image.Width == firstlyWidth)
            {
                var anim = new DoubleAnimation();
                anim.From = firstlyWidth;
                anim.To = firstlyWidth * 1.5;
                anim.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                image.BeginAnimation(WidthProperty, anim);
            }
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
            var image = (Image)sender;

            var result = MessageBox.Show("Желаете сохранить картинку?", "Работа со вложениями", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Filter = "Image Files (*.bmp, *.jpg, *png, *jpeg)|*.bmp, *.jpg, *png, *jpeg";
                fileDialog.FileName = "image1212";
                fileDialog.DefaultExt = ".jpg";
                if (fileDialog.ShowDialog() == true)
                {
                    var path = fileDialog.FileName;
                }
            }
        }
    }
}
