using Chairman_Client.Server.Chairman.Functions;
using Multi_Server_Test.Server;
using MVVM_Pattern_Test.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                    var response = await functions.AddNews(NewPost);
                    Notice = response;
                    MessageBox.Show(Notice);
                }, (obj) => NewPost != null && JumboServer.ActiveServer.ActiveUser != null);
            }
        }
        #endregion

    }
}
