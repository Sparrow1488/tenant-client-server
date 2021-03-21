using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.Pages.HomePages.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.ViewModels.LettersViewModels
{
    public class ReadLetterVM : BaseVM
    {
        #region Constructor
        public ReadLetterVM(Letter readLetter)
        {
            ReadLetter = readLetter;
        }
        #endregion

        #region Props
        public Letter ReadLetter { get { return _letter; } private set { _letter = value; OnPropertyChanged(); } }
        private Letter _letter;
        public Page ReplyPage
        {
            get { return _replyPage; }
            set { _replyPage = value; OnPropertyChanged(); }
        }
        private Page _replyPage;
        public ObservableCollection<string> SourceTokens
        {
            get { return _sourceTokens; }
            set { _sourceTokens = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _sourceTokens = new ObservableCollection<string>();
        public ObservableCollection<ImageSource> Sources
        {
            get { return _sources; }
            set { _sources = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ImageSource> _sources = new ObservableCollection<ImageSource>();
        public ObservableCollection<string> OtherDocuments
        {
            get { return _otherDocuments; }
            set { _otherDocuments = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _otherDocuments = new ObservableCollection<string>();
        public string SelectedToken
        {
            get { return _selectedToken; }
            set { _selectedToken = value; OnPropertyChanged(); }
        }
        private string _selectedToken;
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; OnPropertyChanged(); } }
        #endregion

        #region Commands
        public MyCommand RecieveSource
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    SourceTokens =  await ReciveSourceByToken();
                }, (obj) => ReadLetter != null);
            }
        }
        public MyCommand ShowReplyPage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    ReplyPage = new ResponseToLetterPage(ReadLetter.Id);
                }, (obj) => ReadLetter != null);
            }
        }
        public MyCommand SaveImage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    Console.WriteLine("что");
                }, (obj) => ReadLetter != null);
            }
        }
        #endregion


        #region Methods
        private string path = @"C:\Users\Dom\Desktop\MyContent\img.png";
        private async Task<ObservableCollection<string>> ReciveSourceByToken()
        {
            if (ReadLetter == null) return SourceTokens;
            foreach (var token in ReadLetter.SourcesTokens)
            {
                SourceTokens.Add(token);
                var source = await JumboServer.ActiveServer.GetSourceByToken(token);
                if (source == null) continue;
                var sourceData = Convert.FromBase64String(source?.Data);
                try { Sources.Add(BitmapFrame.Create(new MemoryStream(sourceData))); }
                catch { OtherDocuments.Add("Какое то вложение"); }
            }
            return SourceTokens;
            //File.WriteAllBytes(path, sourceData);
            //imageControl.Source = BitmapFrame.Create(new MemoryStream(File.ReadAllBytes(@"C:\Users\Dom\Downloads\profile.png")));
        }
        #endregion

    }
}
