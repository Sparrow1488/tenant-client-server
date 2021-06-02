using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages.Default;
using MVVM_Pattern_Test.ClientEntities;
using MVVM_Pattern_Test.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MVVM_Pattern_Test.ViewModels
{
    public class AttachmentsVM : BaseVM
    {
        #region Constructor
        public AttachmentsVM(int[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                SourceTokens = ids.ToList();
                RecieveSource.Execute(null);
            }
        }
        #endregion

        #region Props
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; } }
        public List<int> SourceTokens
        {
            get { return _sourceTokens; }
            set { _sourceTokens = value; OnPropertyChanged(); }
        }
        private List<int> _sourceTokens = new List<int>();
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
                            File.WriteAllBytes(fileName, Convert.FromBase64String(source.Base64Data));
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
            var manager = new ExSysManager();
            
            var response = await manager.GetSource(SourceTokens.ToArray());
            if (response.Status == ResponseStatus.Ok)
            {
                var sources = response.ResponseData as Source[];
                FilterSource(sources);
            }
        }
        private List<string> Extensions = new List<string>() { ".png", ".jpg", ".jpeg" };
        private void FilterSource(Source[] sources)
        {
            foreach (var source in sources)
            {
                if (source == null) continue;
                byte[] sourceData = Convert.FromBase64String(source?.Base64Data);
                AttachmentsSource.Add(source);
                try { ImageSources.Add(BitmapFrame.Create(new MemoryStream(sourceData))); }
                catch { OtherDocuments.Add(source); }
            }
        }
        #endregion
    }
}
