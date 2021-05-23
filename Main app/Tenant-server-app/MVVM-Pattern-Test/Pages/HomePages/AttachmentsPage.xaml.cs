using Microsoft.Win32;
using MVVM_Pattern_Test.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MVVM_Pattern_Test.Pages.HomePages
{
    /// <summary>
    /// Логика взаимодействия для AttachmentsPage.xaml
    /// </summary>
    public partial class AttachmentsPage : Page
    {
        public AttachmentsPage(List<string> sourceTokens)
        {
            InitializeComponent();
            DataContext = new AttachmentsVM(sourceTokens);
        }
        public AttachmentsPage(string[] sourceTokens)
        {
            InitializeComponent();
            DataContext = new AttachmentsVM(sourceTokens);
        }
        private double firstlyHeight = 60;
        private void attachedImage_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        private void attachedImage_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
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
