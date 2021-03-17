using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.Pages.HomePages.Admin;
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

            AdminReaderPage = new AdminReaderPage();
            AdminNewsWriterPage = new NewsWriterPage();
            //AdminLettersPage = new AdminLettersPage();

            ActivePage = ProfilePage;
        }
        #endregion

        #region Props
        public override string Notice
        {
            get { return _infoMessage; }
            protected set { _infoMessage = value; OnPropertyChanged(); }
        }

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

        #region ADMIN_REGINON
        public Page AdminReaderPage
        {
            get { return _adminReaderPage; }
            private set { _adminReaderPage = value; OnPropertyChanged(); }
        }
        private Page _adminReaderPage;
        public Page AdminNewsWriterPage
        {
            get { return _adminNewsWriterPage; }
            private set { _adminNewsWriterPage = value; OnPropertyChanged(); }
        }
        private Page _adminNewsWriterPage;
        #endregion AdminRegion
        
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
        public MyCommand ShowProfilePage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    ActivePage = ProfilePage;
                }, (obj) => ProfilePage != null);
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
        public MyCommand ShowAdminReaderPage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    ActivePage = AdminReaderPage;
                }, (obj) => AdminReaderPage != null);
            }
        }

        #endregion
    }
}
