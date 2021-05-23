using ExchangeSystem.Requests.Objects.Entities;
using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.Pages.HomePages;
using MVVM_Pattern_Test.Pages.HomePages.Admin;
using System.Windows.Controls;

namespace MVVM_Pattern_Test.ViewModels.LettersViewModels
{
    public class ReadLetterVM : BaseVM
    {
        #region Constructor
        public ReadLetterVM(Letter readLetter)
        {
            ReadLetter = readLetter;

            ShowAttachesPage.Execute(null);
        }
        #endregion

        #region Props
        public Letter ReadLetter { get { return _letter; } private set { _letter = value; OnPropertyChanged(); } }
        private Letter _letter;
        public Page ReplyPage
        {
            get { return _replyPage; }
            set { _replyPage = value; OnPropertyChanged(); }
        }
        private Page _replyPage;
        public Page AttachPage
        {
            get { return _attachPage; }
            set { _attachPage = value; OnPropertyChanged(); }
        }
        private Page _attachPage;
        
        public string SelectedToken
        {
            get { return _selectedToken; }
            set { _selectedToken = value; OnPropertyChanged(); }
        }
        private string _selectedToken;
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; OnPropertyChanged(); } }
        #endregion

        #region Commands
        
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
        public MyCommand ShowAttachesPage
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    //AttachPage = new AttachmentsPage(ReadLetter?.SourcesTokens);
                }, (obj) => ReadLetter != null);
            }
        }
        #endregion
    }
}
