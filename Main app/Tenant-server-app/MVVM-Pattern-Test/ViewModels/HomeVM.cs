using ExchangeSystem.Requests.Objects.Entities;
using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.Pages.HomePages;
using MVVM_Pattern_Test.Pages.HomePages.Admin;
using System.Windows.Controls;
using WpfApp1.Pages.HomePages;

namespace MVVM_Pattern_Test.ViewModels
{
    public class HomeVM : BaseVM
    {
        #region Constructor
        public HomeVM(User user)
        {
            if(user != null)
            {
                AuthUser = user;
                ShowProfilePage.Execute(null);
            }
        }
        #endregion

        #region Props
        public override string Notice
        {
            get { return _infoMessage; }
            protected set { _infoMessage = value; OnPropertyChanged(); }
        }
        public User AuthUser {
            get { return _authUser; }
            private set { _authUser = value; OnPropertyChanged(); }
        }
        private User _authUser;

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
        public Page SettingsPage
        {
            get { return _settingsPage; }
            set { _settingsPage = value; OnPropertyChanged(); }
        }
        private Page _settingsPage;


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
                    if (LettersPage == null) LettersPage = new LetterPage();
                    ActivePage = LettersPage;
                });
            }
        }
        public MyCommand ShowProfilePage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    if (ProfilePage == null) ProfilePage = new ProfilePage(AuthUser);
                    ActivePage = ProfilePage;
                });
            }
        }
        public MyCommand ShowNewsPage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    if (NewsPage == null) NewsPage = new NoticePage();
                    ActivePage = NewsPage;
                });
            }
        }
        public MyCommand ShowAdminReaderPage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    if (AdminReaderPage == null) AdminReaderPage = new AdminReaderPage();
                    ActivePage = AdminReaderPage;
                });
            }
        }
        public MyCommand ShowAdminNewsWriterPage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    if (AdminNewsWriterPage == null) AdminNewsWriterPage = new NewsWriterPage();
                    ActivePage = AdminNewsWriterPage;
                });
            }
        }
        public MyCommand ShowSettingsPage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    if (SettingsPage == null) SettingsPage = new SettingsPage();
                    ActivePage = SettingsPage;
                });
            }
        }
        #endregion
    }
}
