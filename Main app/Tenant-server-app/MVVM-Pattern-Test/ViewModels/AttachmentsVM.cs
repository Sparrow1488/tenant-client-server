using MVVM_Pattern_Test.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.Packages.SourceDir;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.ViewModels
{
    public class AttachmentsVM : BaseVM
    {
        #region Constructor
        public AttachmentsVM(List<string> tokens)
        {
            if(tokens != null && tokens.Count > 0)
            {
                SourceTokens = tokens;
                RecieveSource.Execute(null);
            }
        }
        public AttachmentsVM(string[] tokens)
        {
            if (tokens != null && tokens.Length > 0)
            {
                foreach (var token in tokens)
                    SourceTokens.Add(token);
                RecieveSource.Execute(null);
            }
        }
        #endregion

        #region Props
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; } }
        public List<string> SourceTokens
        {
            get { return _sourceTokens; }
            set { _sourceTokens = value; OnPropertyChanged(); }
        }
        private List<string> _sourceTokens = new List<string>();
        public ObservableCollection<ImageSource> ImageSources
        {
            get { return _imageSources; }
            set { _imageSources = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ImageSource> _imageSources = new ObservableCollection<ImageSource>();
        public ObservableCollection<Source> OtherDocuments
        {
            get { return _otherDocuments; }
            set { _otherDocuments = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Source> _otherDocuments = new ObservableCollection<Source>();
        public List<Source> AttachmentsSource
        {
            get { return _attachmentsSource; }
            set { _attachmentsSource = value; OnPropertyChanged(); }
        }
        private List<Source> _attachmentsSource = new List<Source>();
        #endregion

        #region Commands
        public MyCommand RecieveSource
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    await ReciveSourceByToken();
                });
            }
        }
        private string directoryPath = @".\Downloads\";
        public MyCommand SaveAll
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    Random rnd = new Random();
                    string downloadArchivePath = directoryPath + @"\" + "Docs" + DateTime.Now.ToBinary() + "_" + rnd.Next(1000, 99999).ToString() + @"\";
                    try
                    {
                        foreach (var source in AttachmentsSource)
                        {
                            DirectoryInfo info = new DirectoryInfo(directoryPath);
                            if (!info.Exists) Directory.CreateDirectory(directoryPath);
                            Directory.CreateDirectory(downloadArchivePath);
                            var rndNum = rnd.Next(10000, 60000);
                            string fileName = $"{downloadArchivePath}{source?.Extension.Replace('.', ' ').ToUpper()}_{rndNum}{source?.Extension}";
                            File.WriteAllBytes(fileName, Convert.FromBase64String(source.Data));
                        }
                        Notice = "Файлы успешно загружены";
                    }
                    catch { }
                }, (obj) => AttachmentsSource != null && AttachmentsSource.Count > 0);
            }
        }
        #endregion

        #region Methods
        private async Task ReciveSourceByToken()
        {
            foreach (var token in SourceTokens)
            {
                var source = await JumboServer.ActiveServer.GetSourceByToken(token);
                if (source == null) continue;
                byte[] sourceData = Convert.FromBase64String(source?.Data);
                AttachmentsSource.Add(source);
                try { ImageSources.Add(BitmapFrame.Create(new MemoryStream(sourceData))); }
                catch { OtherDocuments.Add(source); }
            }
        }
        #endregion
    }
}
