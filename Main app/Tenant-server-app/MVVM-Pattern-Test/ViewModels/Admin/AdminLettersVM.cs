using Chairman_Client.Server.Chairman.Functions;
using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.Pages.HomePages.ChildLetterPage;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.ViewModels.Admin
{
    public class AdminLettersVM : BaseVM
    {
        #region Constructor
        public AdminLettersVM()
        {
            ReadLetterPage = new ReadLetterPage(SelectedLetter);
        }
        #endregion

        #region Props
        public override string Notice
        {
            get { return _infoMessage; }
            protected set { _infoMessage = value; OnPropertyChanged(); }
        }
        public List<Letter> AllLetters 
        {
            get { return _allLetters; }
            set { _allLetters = value; OnPropertyChanged(); }
        }
        private List<Letter> _allLetters = new List<Letter>();
        public Letter SelectedLetter
        {
            get { return _selectedLetter; }
            set { _selectedLetter = value; ReadLetterPage = new ReadLetterPage(_selectedLetter); OnPropertyChanged();  }
        }
        private Letter _selectedLetter;
        private Functions functions = new Functions(JumboServer.ActiveServer);
        #endregion

        #region Pages
        public Page ReadLetterPage
        {
            get { return _readLetterPage; }
            set { _readLetterPage = value; OnPropertyChanged(); }
        }
        private Page _readLetterPage;
        #endregion

        #region Commands
        public MyCommand RecieveAllLetters
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    var responseLetters = await functions.GetLetters();
                    if (responseLetters != null)
                    {
                        Notice = "Все письма успешно получены";
                        AllLetters = responseLetters;
                    }
                    else
                        Notice = "Список писем пока пуст";
                    MessageBox.Show(Notice);
                }, (obj) => functions != null);
            }
        }
        #endregion

    }
}
