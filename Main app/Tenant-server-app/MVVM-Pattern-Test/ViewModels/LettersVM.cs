using MVVM_Pattern_Test.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.ViewModels
{
    public class LettersVM : BaseVM
    {
        #region Constructor
        public LettersVM()
        {

        }
        #endregion

        #region Props
        public override string Notice
        {
            get { return _infoMessage; }
            protected set { _infoMessage = value; OnPropertyChanged(); }
        }

        public List<Letter> MyLetters
        {
            get { return _myLetter; }
            private set { _myLetter = value; OnPropertyChanged(); }
        }
        public List<Letter> _myLetter = new List<Letter>();
        #endregion

        #region Commands
        public MyCommand RecieveSendLetters
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    var listLetters = await JumboServer.ActiveServer.GetMyLetters();
                });
            }
        }
        #endregion


    }
}
