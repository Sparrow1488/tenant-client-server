using Microsoft.Win32;
using MVVM_Pattern_Test.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.Packages.SourceDir;
using WpfApp1.Server.ServerExceptions;
using WpfApp1.Server.ServerMeta;
using System.Collections.ObjectModel;
using MVVM_Pattern_Test.MyApplication;

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
        private ClientFunctions funcs = new ClientFunctions();
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
                        var tokensCollection = new List<string>(AttachmentsTokens);
                        Letter sendLetter = null;
                        if (AttachmentsTokens == null)
                            sendLetter = new Letter(Title, Description, JumboServer.ActiveServer.ActiveUser.Id, 0);
                        else sendLetter = new Letter(Title, Description, JumboServer.ActiveServer.ActiveUser.Id, tokensCollection.ToArray(), 0);
                        
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
                    string fileExtension = string.Empty;
                    funcs.OpenFile(out base64Data, out fileExtension);
                    if (!string.IsNullOrWhiteSpace(base64Data))
                    {
                        var newSource = new Source(base64Data, JumboServer.ActiveServer.ActiveUser.Id, fileExtension);
                        Notice = "Загрузка...";
                        AttachWasAttached = false;
                        var sourceToken = await JumboServer.ActiveServer.AddSource(newSource);

                        if (!string.IsNullOrWhiteSpace(sourceToken))
                        {
                            AttachmentsTokens.Add(sourceToken);
                            Notice = "Файл успешно добавлен";
                        }
                        else
                            Notice = "Ошибка загрузки файла";
                    }
                    else
                        Notice = "Не удалось закодировать данные";
                    AttachWasAttached = true;
                }, (obj) => AttachmentsTokens.Count < 5 && AttachWasAttached);
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
