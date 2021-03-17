using Chairman_Client.Server.Chairman.Functions;
using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client.ViewModels
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
        public List<Letter> AllLetters 
        {
            get { return _allLetters; }
            set { _allLetters = value; OnPropertyChanged(); }
        }
        private List<Letter> _allLetters = new List<Letter>();
        private Functions functions = new Functions("secret", JumboServer.ActiveServer);
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
