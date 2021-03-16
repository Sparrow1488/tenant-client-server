using Chairman_Client.Pages;
using MVVM_Pattern_Test.Commands;
using System.Windows.Controls;

namespace MVVM_Pattern_Test.ViewModels
{
    public class HomeVM : BaseVM
    {
        #region Constructor
        public HomeVM()
        {
            LettersPage = new LetterPage();
            NewsPage = new NewsPage();

            ActivePage = NewsPage;
        }
        #endregion

        #region Props
        public override string Notice
        {
            get { return _infoMessage; }
            protected set { _infoMessage = value; OnPropertyChanged(); }
        }
        public Page NewsPage
        {
            get { return _newsPage; }
            private set { _newsPage = value; OnPropertyChanged(); }
        }
        private Page _newsPage;
        public Page LettersPage
        {
            get { return _lettersPage; }
            private set { _lettersPage = value; OnPropertyChanged(); }
        }
        private Page _lettersPage;
        public Page ActivePage
        {
            get { return _activePage; }
            set { _activePage = value; OnPropertyChanged(); }
        }
        private Page _activePage;
        #endregion

        #region Commands
        public MyCommand ShowLettersPage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    ActivePage = LettersPage;
                }, (obj) => LettersPage != null);
            }
        }
        public MyCommand ShowNewsPage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    ActivePage = NewsPage;
                }, (obj) => NewsPage != null);
            }
        }

        #endregion
    }
}
