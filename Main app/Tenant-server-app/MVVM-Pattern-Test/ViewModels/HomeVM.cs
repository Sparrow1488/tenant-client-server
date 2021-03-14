using MVVM_Pattern_Test.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Pages.HomePages;

namespace MVVM_Pattern_Test.ViewModels
{
    public class HomeVM : BaseVM
    {
        #region Constructor
        public HomeVM()
        {
            ProfilePage = new ProfilePage();
            LettersPage = new LetterPage();
            NewsPage = new NoticePage();

            ActivePage = ProfilePage;
        }
        #endregion

        #region Props
        public Page ProfilePage
        {
            get { return _profilePage; }
            private set { _profilePage = value; OnPropertyChanged(); }
        }
        private Page _profilePage;
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
        public MyCommand SelectPage
        {
            get
            {
                return new MyCommand((obj) =>
                {

                }, (obj) => ActivePage != null);
            }
        }
        #endregion
    }
}
