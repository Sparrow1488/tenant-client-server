using MVVM_Pattern_Test.Commands;
using System;

namespace MVVM_Pattern_Test.ViewModels.Admin
{
    public class AdminReplyerVM : BaseVM
    {
        #region Constructor
        public AdminReplyerVM(int letterID)
        {
            //if (JumboServer.ActiveServer == null)
            //    throw new ArgumentNullException("Вы не можете создавать расширенные функции, если значение объекта сервера равно Null");
            //functions = new Functions(JumboServer.ActiveServer);
            //_letterId = letterID;
        }
        #endregion

        #region Props
        private int _letterId;
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; OnPropertyChanged(); } }
        public string Answer { get; set; } = "";
        #endregion

        #region Commands
        public MyCommand DoReply
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    //var newReply = new ReplyLetter(Answer, JumboServer.ActiveServer.ActiveUser.Id, _letterId);
                    //if(newReply != null)
                    //{
                    //    var response = await functions.SendReplyToLetter(newReply);
                    //    if (response == "1")
                    //    {
                    //        Notice = "Ответ отправлен";
                    //        Answer = "";
                    //    }
                    //    else Notice = "Ошибка отправки ответа";
                    //}
                });
            }
        }
        #endregion
    }
}
