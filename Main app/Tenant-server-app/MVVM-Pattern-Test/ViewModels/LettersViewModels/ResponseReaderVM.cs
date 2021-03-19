using Chairman_Client.Server.Packages.LettersDir;
using MVVM_Pattern_Test.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.ViewModels.LettersViewModels
{
    public class ResponseReaderVM : BaseVM
    {
        #region Constructor
        public ResponseReaderVM(int letterId)
        {
            _actualLetterId = letterId;
            RecieveLetterResponses.Execute(null);
        }
        #endregion
        #region Props
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; OnPropertyChanged(); } }
        private int _actualLetterId;
        public List<ReplyLetter> AllResponses
        {
            get { return _allResponses; }
            set { _allResponses = value; OnPropertyChanged(); }
        }
        private List<ReplyLetter> _allResponses = new List<ReplyLetter>();
        #endregion
    
        public MyCommand RecieveLetterResponses
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    var responses = await JumboServer.ActiveServer.GetReplyByLetterId(_actualLetterId);
                    if (responses != null)
                    {
                        AllResponses = responses;
                    }
                    else MessageBox.Show("Попка", "Ответ");
                });
            }
        }
    }
}
