using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.Packages.SourceDir;
using WpfApp1.Server.ServerMeta;

namespace WpfApp1.Pages.HomePages.ChildLetterPage
{
    /// <summary>
    /// Логика взаимодействия для UniversalLetterPage.xaml
    /// </summary>
    public partial class UniversalLetterPage : Page
    {
        private List<string> UploadedSourceTokens = new List<string>();
        public UniversalLetterPage()
        {
            InitializeComponent();
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
            var btn = (Button)sender;
            btn.IsEnabled = false;

            string result = string.Empty;
            try
            {
                var sendLetter = SelectLetterType(titleBox.Text, descBox.Text, JumboServer.ActiveServer.ActiveUser.Id, UploadedSourceTokens);
                if (sendLetter != null)
                {
                    var letterSender = JumboServer.ActiveServer.ActiveUser.Login;
                    
                    result = await JumboServer.ActiveServer.SendLetter(sendLetter); //TODO: сделать нормальный ответ от сервера (прим.: 1-успешно, 2-ошибка и тд)
                    if(result == "1")
                    {
                        //LetterPage.ShowMessage("Письмо успешно добавлено");
                        UploadedSourceTokens.Clear();
                        sourceAtteched.Items.Clear();
                        sourceAtteched.Visibility = Visibility.Collapsed;
                        titleBox.Text = "";
                        descBox.Text = "";
                    }
                    else
                    {
                        //LetterPage.ShowExceptionMessage("Возникла ошибка при получении письма");
                    }
                        
                }
            }
            catch (Exception ex)
            {
                //LetterPage.ShowExceptionMessage(ex.Message);
            }
            finally
            {
                btn.IsEnabled = true;
            }
        }
        private Letter SelectLetterType(string title, string desc, int senderId, List<string> sources)
        {
            string[] uploadedSources = new string[5];
            for (int i = 0; i < sources.Count; i++)
            {
                uploadedSources[i] = sources[i];
            }
            if ((bool)offerType.IsChecked)
                return new OfferLetter(title, desc, senderId, uploadedSources);
            if ((bool)complaintType.IsChecked)
                return new ComplaintLetter(title, desc, senderId, uploadedSources);
            if ((bool)questionType.IsChecked)
                return new QuestionLetter(title, desc, senderId, uploadedSources);
            return null;
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
            string base64Data = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;
                base64Data = Convert.ToBase64String(File.ReadAllBytes(filePath));
                FileInfo info = new FileInfo(filePath);
                MessageBox.Show(info.Extension, "File extension");
            }
            if (!string.IsNullOrWhiteSpace(base64Data))
            {
                var newSource = new Source(base64Data, JumboServer.ActiveServer.ActiveUser.Id);
                var sourceToken = await JumboServer.ActiveServer.AddSource(newSource);
                if (!string.IsNullOrWhiteSpace(sourceToken))
                {
                    AddInAttechedList(sourceToken);
                    MessageBox.Show("Токен вложения: " + sourceToken + "\n" + "Вложений всего: " + UploadedSourceTokens.Count, "Response source token");
                }
                else
                    MessageBox.Show(sourceToken, "Exception upload source");

            }
            else
                MessageBox.Show("Не удалось закодировать данные", "Exception encoding");
        }

        private void AddInAttechedList(string token)
        {
            sourceAtteched.Visibility = Visibility.Visible;
            UploadedSourceTokens.Add(token);
            sourceAtteched.Items.Add("Source token: " + token);
        }
    }
}
