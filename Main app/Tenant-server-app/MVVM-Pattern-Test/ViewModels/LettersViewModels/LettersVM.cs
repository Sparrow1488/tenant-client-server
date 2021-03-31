using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.Pages.HomePages.ChildLetterPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Pages.HomePages.ChildLetterPage;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.ViewModels.LettersViewModels
{
    public class LettersVM : BaseVM
    {
        #region Constructor
        public LettersVM()
        {
            RecieveMyLetters.Execute(null);

            UniversalPage = new UniversalLetterPage();
            ReadLetterPage = new ReadLetterPage(SelectedLetter);
        }
        #endregion

        #region Props
        public override string Notice
        {
            get { return _infoMessage; }
            protected set { _infoMessage = value; OnPropertyChanged(); }
        }

        public Letter NewLetter
        {
            get { return _newLetter; }
            private set { _newLetter = value; OnPropertyChanged(); }
        }
        private Letter _newLetter;
        public Letter SelectedLetter
        {
            get { return _selectedLetter; }
            set 
            { 
                _selectedLetter = value; ReadLetterPage = new ReadLetterPage(SelectedLetter);
                ResponseReader = new ReplyReaderPage(SelectedLetter.Id);
                OnPropertyChanged();
            }
        }
        private Letter _selectedLetter;
        public Page SelectedPage
        {
            get { return _selectedPage; }
            private set { _selectedPage = value; OnPropertyChanged(); }
        }
        private Page _selectedPage;
        public Page UniversalPage
        {
            get { return _universalPage; }
            private set { _universalPage = value; OnPropertyChanged(); }
        }
        private Page _universalPage;
        public Page ReadLetterPage
        {
            get { return _readLetterPage; }
            set { _readLetterPage = value; OnPropertyChanged(); }
        }
        private Page _readLetterPage;
        public List<Letter> MyLetters
        {
            get { return _myLetter; }
            private set { _myLetter = value; OnPropertyChanged(); }
        }
        public List<Letter> _myLetter = new List<Letter>();
        public Page ResponseReader
        { 
            get { return _responseReaderPage; }
            set { _responseReaderPage = value; OnPropertyChanged(); }
        }
        private Page _responseReaderPage;
        #endregion

        #region Commands
        public MyCommand RecieveMyLetters
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    MyLetters = new List<Letter>();
                    MyLetters = await JumboServer.ActiveServer.GetMyLetters();
                });
            }
        }
        public MyCommand ShowUniversalLetterPage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    SelectedPage = UniversalPage;
                }, (obj) => UniversalPage != null);
            }
        }
        public MyCommand ShowResponseReaderPage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    ResponseReader = new ReplyReaderPage(SelectedLetter.Id);
                }, (obj) => SelectedLetter != null);
            }
        }
        #endregion

        #region Methods
        private void AnimationShowPage(Page page)
        {

        }
        #endregion
    }
}
