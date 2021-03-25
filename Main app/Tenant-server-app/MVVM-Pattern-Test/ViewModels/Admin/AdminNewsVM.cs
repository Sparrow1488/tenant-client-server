using Chairman_Client.Server.Chairman.Functions;
using Multi_Server_Test.Server;
using MVVM_Pattern_Test.Commands;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.ViewModels.Admin
{
    public class AdminNewsVM : BaseVM
    {
        #region Constructor
        public AdminNewsVM()
        {
            var sender = JumboServer.ActiveServer.ActiveUser;
            NewPost = new News("Заголовок", "Описание", sender.Id, null, "testtest");
            NewPost.Sender = sender.Login;
        }
        #endregion

        #region Props
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; OnPropertyChanged(); } }
        public News NewPost
        {
            get { return _post; }
            set { _post = value; OnPropertyChanged(); }
        }
        private News _post;
        private Functions functions = new Functions(JumboServer.ActiveServer);
        #endregion

        #region Commands
        public MyCommand SendNews
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    var requestResult = await functions.AddNews(NewPost);
                    PrintServerResult(requestResult);
                }, (obj) => NewPost != null && JumboServer.ActiveServer.ActiveUser != null);
            }
        }
        #endregion

        #region OtherMetods
        public void PrintServerResult(string requestResult)
        {
            if (requestResult == "1")
            {
                Notice = "Новость успешно добавлена";
                ToBaseForm();
            }
            else if (requestResult == "0")
                Notice = "Ошибка при проверке на валидацию";
            else
                Notice = "Неизвестная ошибка сервера";
        }
        public void ToBaseForm()
        {
            NewPost.Title = "";
            NewPost.Description = "";
        }
        #endregion
    }
}
