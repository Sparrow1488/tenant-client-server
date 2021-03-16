using MVVM_Pattern_Test.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Pages.HomePages.ChildLetterPage;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.ViewModels
{
    public class LettersVM : BaseVM
    {
        #region Constructor
        public LettersVM()
        {
            RecieveMyLetters.Execute(null);

            UniversalPage = new UniversalLetterPage();
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
        public List<Letter> MyLetters
        {
            get { return _myLetter; }
            private set { _myLetter = value; OnPropertyChanged(); }
        }
        public List<Letter> _myLetter = new List<Letter>();
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
        #endregion

        #region Methods
        private void AnimationShowPage(Page page)
        {

        }
        #endregion
    }
}
