using Microsoft.Win32;
using MVVM_Pattern_Test.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.Packages.SourceDir;
using WpfApp1.Server.ServerExceptions;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.ViewModels.LettersViewModels
{
    public class LetterSenderVM : BaseVM
    {
        #region Constructor
        public LetterSenderVM()
        {
            AttachmentsTokens = new List<string>();
        }
        #endregion

        #region Props
        public override string Notice 
        { 
            get { return _infoMessage; }
            protected set { _infoMessage = value; OnPropertyChanged(); }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        private string _title = ""; 
        public string Description
        {
            get { return _desc; }
            set { _desc = value; OnPropertyChanged(); }
        }
        private string _desc = "";
        public List<string> AttachmentsTokens
        {
            get { return _attachments; }
            set { _attachments = value; OnPropertyChanged(); }
        }
        private List<string> _attachments;
        private bool AttachWasAttached = true;
        #endregion

        #region Commands
        public MyCommand SendLetter
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    string result = string.Empty;
                    try
                    {
                        Letter sendLetter = null;
                        if (AttachmentsTokens == null)
                            sendLetter = new Letter(Title, Description, JumboServer.ActiveServer.ActiveUser.Id, 0);
                        else sendLetter = new Letter(Title, Description, JumboServer.ActiveServer.ActiveUser.Id, AttachmentsTokens.ToArray(), 0);
                        
                        var letterSender = JumboServer.ActiveServer.ActiveUser.Login;
                        result = await TrySendLetter(sendLetter);
                        if (result == "1")
                        {
                            Title = "";
                            Description = "";
                            AttachmentsTokens = new List<string>();
                            Notice = "Письмо успешно доставлено";
                        }
                        else
                        {
                            Notice = "Возникла ошибка при получении письма";
                        }
                    }
                    catch (JumboServerException ex)
                    {
                        Notice = ex.Message;
                    }
                }, (obj) => AttachWasAttached);
            }
        }
        public MyCommand AttachFile
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    string base64Data = string.Empty;
                    OpenFileDialog dialog = new OpenFileDialog();
                    if (dialog.ShowDialog() == true)
                    {
                        string filePath = dialog.FileName;
                        try { base64Data = Convert.ToBase64String(File.ReadAllBytes(filePath)); }
                        catch { /* НЕВЕРНЫЙ ФОРМАТЬ ДОКУМЕНТА*/ }
                        FileInfo info = new FileInfo(filePath);
                        MessageBox.Show("Вы пытаетесь прикрепить файл с расширением: " + info.Extension, "Подтвердите отправку", MessageBoxButton.YesNo);
                    }
                    if (!string.IsNullOrWhiteSpace(base64Data))
                    {
                        var newSource = new Source(base64Data, JumboServer.ActiveServer.ActiveUser.Id);
                        var sourceToken = await JumboServer.ActiveServer.AddSource(newSource);
                        if (!string.IsNullOrWhiteSpace(sourceToken))
                        {
                            AttachmentsTokens.Add(sourceToken);
                            MessageBox.Show("Токен вложения: " + sourceToken + "\n" + "Вложений всего: " + AttachmentsTokens.Count, "Вложение успешно добавлено");
                        }
                        else
                            MessageBox.Show(sourceToken, "Exception upload source");
                    }
                    else
                        MessageBox.Show("Не удалось закодировать данные", "Exception encoding");
                }, (obj) => AttachmentsTokens.Count < 5);
            }
        }
        #endregion

        #region Methods
        private async Task<string> TrySendLetter(Letter letter)
        {
            string response = await JumboServer.ActiveServer.SendLetter(letter); //TODO: сделать нормальный ответ от сервера (прим.: 1-успешно, 2-ошибка и тд)
            return response;
        }
        #endregion
    }
}
