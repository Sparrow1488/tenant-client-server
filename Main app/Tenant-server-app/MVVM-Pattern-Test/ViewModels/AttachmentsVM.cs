﻿using MVVM_Pattern_Test.Commands;
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
        public AttachmentsVM(Letter readLetter)
        {
            ReadLetter = readLetter;
            RecieveSource.Execute(null);
        }
        #endregion

        #region Props
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; } }
        public Letter ReadLetter { get { return _letter; } private set { _letter = value; OnPropertyChanged(); } }
        private Letter _letter;

        public ObservableCollection<string> SourceTokens
        {
            get { return _sourceTokens; }
            set { _sourceTokens = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _sourceTokens = new ObservableCollection<string>();
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
        public Dictionary<string, byte[]> DataByAttachments
        {
            get { return _dataByAttachments; }
            set { _dataByAttachments = value; OnPropertyChanged(); }
        }
        private Dictionary<string, byte[]> _dataByAttachments = new Dictionary<string, byte[]>();
        #endregion

        #region Commands
        public MyCommand RecieveSource
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    SourceTokens = await ReciveSourceByToken();
                }, (obj) => ReadLetter != null);
            }
        }
        private string path = "./Downloads/";
        public MyCommand SaveAll
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    Random rnd = new Random();
                    foreach (var file in DataByAttachments)
                    {
                        var num = rnd.Next(20000, 60000);
                        string fileName = $"{path}{file.Key.Replace('.', ' ').ToUpper()}_{num}{file.Key}";
                        File.WriteAllBytes(fileName, file.Value);
                    }
                    Notice = "Файлы успешно загружены";
                }, (obj) => ReadLetter != null && DataByAttachments != null && DataByAttachments.Count > 0);
            }
        }
        #endregion

        #region Methods
        private async Task<ObservableCollection<string>> ReciveSourceByToken()
        {
            if (ReadLetter == null) return SourceTokens;
            foreach (var token in ReadLetter.SourcesTokens)
            {
                SourceTokens.Add(token);
                var source = await JumboServer.ActiveServer.GetSourceByToken(token);
                if (source == null) continue;
                byte[] sourceData = Convert.FromBase64String(source?.Data);
                DataByAttachments.Add(source.Extension, sourceData);
                try { ImageSources.Add(BitmapFrame.Create(new MemoryStream(sourceData))); }
                catch { OtherDocuments.Add(source); }
            }
            return SourceTokens;
        }
        #endregion
    }
}