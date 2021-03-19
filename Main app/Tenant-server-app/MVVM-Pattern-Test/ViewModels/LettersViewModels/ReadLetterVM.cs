using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.Pages.HomePages.Admin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp1.Server.Packages.Letters;

namespace MVVM_Pattern_Test.ViewModels.LettersViewModels
{
    public class ReadLetterVM : BaseVM
    {
        public ReadLetterVM(Letter readLetter)
        {
            ReadLetter = readLetter;
        }
        public Letter ReadLetter { get { return _letter; } private set { _letter = value; OnPropertyChanged(); } }
        private Letter _letter;
        public Page ReplyPage
        {
            get { return _replyPage; }
            set { _replyPage = value; OnPropertyChanged(); }
        }
        private Page _replyPage;
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; OnPropertyChanged(); } }
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
        private string MyPath 
        { 
            get { return @"C:\Users\Dom\Desktop\Репозитории\asdasd.png"; } 
            set 
            {
                Random rnd = new Random();
                var rndNum = rnd.Next(10000, 999999);
                MyPath = value; OnPropertyChanged(); 
            } 
        }
        //public ImageSource MyImagee
        //{
        //    get { return _MyImagee; }
        //    set
        //    {
        //        _MyImagee = value; OnPropertyChanged();
        //    }
        //}
        //private ImageSource _MyImagee = new BitmapImage(new Uri(@"C:\Users\Dom\Desktop\Репозитории\asdasd.png"));
        public List<System.Windows.Controls.Image> Images
        {
            get { return _images; }
            set
            {
                _images = value; /*OnPropertyChanged();*/
            }
        }
        private List<System.Windows.Controls.Image> _images = new List<System.Windows.Controls.Image>();
        public List<ImageSource> MySources
        {
            get { return _MySources; }
            set
            {
                _MySources = value; OnPropertyChanged();
            }
        }
        private List<ImageSource> _MySources = new List<ImageSource>();
        private async Task ReciveSourceByToken()
        {
            if (ReadLetter == null) return;
            foreach (var token in ReadLetter?.SourcesTokens)
            {
                ImageSource d = new BitmapImage(new Uri(MyPath));
                //var source = await JumboServer.ActiveServer.GetSourceByToken(token);
                //Image image = Image.FromStream(new MemoryStream(Convert.FromBase64String(source.Data)));
                //image.Save(MyPath);
            }
        }
    }
}
