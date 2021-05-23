using ExchangeSystem.Requests.Objects.Entities;
using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.MyApplication;
using System.Collections.Generic;

namespace MVVM_Pattern_Test.ViewModels.Admin
{
    public class AdminNewsVM : BaseVM
    {
        #region Constructor
        public AdminNewsVM()
        {
            NewPost = new Publication();
            NewPost.SenderId = 0;
        }
        #endregion

        #region Props
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; OnPropertyChanged(); } }
        public Publication NewPost
        {
            get { return _post; }
            set { _post = value; OnPropertyChanged(); }
        }
        private Publication _post;
        private ClientFunctions funcs = new ClientFunctions();
        //public List<string> SourceTokens = new List<string>();
        #endregion

        #region Commands
        public MyCommand SendNews
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    //NewPost.Sources = SourceTokens.ToArray();
                    //    var requestResult = await functions.AddNews(NewPost);
                    //    PrintServerResult(requestResult);
                });
            }
        }
        public MyCommand AttachFile
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    //string base64Data = string.Empty;
                    //string extensionFile = string.Empty;
                    //funcs.OpenFile(out base64Data, out extensionFile);
                    //if(!string.IsNullOrWhiteSpace(base64Data) && !string.IsNullOrWhiteSpace(extensionFile))
                    //{
                    //    var sourceToken = await JumboServer.ActiveServer.AddSource(new Source(base64Data, 
                    //                                                                                                JumboServer.ActiveServer.ActiveUser.Id, 
                    //                                                                                                extensionFile));
                    //    if (!string.IsNullOrWhiteSpace(sourceToken))
                    //        SourceTokens.Add(sourceToken);
                    //    Notice = "Файл успешно прикреплен";
                    //}
                });
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
                Notice = "Ошибка публикации: не пройдено валидацию\nУказания: возможно, текст слишком мал для публикации";
            else
                Notice = "Неизвестная ошибка сервера";
        }
        public void ToBaseForm()
        {
            NewPost = new Publication();
        }
        #endregion
    }
}
