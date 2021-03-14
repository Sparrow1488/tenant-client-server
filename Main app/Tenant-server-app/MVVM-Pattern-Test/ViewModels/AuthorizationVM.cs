using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Server;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.ViewModels
{
    public class AuthorizationVM : BaseVM
    {
        #region Constructor
        public AuthorizationVM()
        {
            ServerFunctions = new JumboServer(new ServerConfig());
            LoginInput = "";
            PasswordInput = "";

            AuthorizationWithToken.Execute(null);
        }
        #endregion

        #region AllProps
        public override string Notice
        {
            get { return _infoMessage; }
            protected set { _infoMessage = value; OnPropertyChanged(); }
        }

        public Action CloseAuthWindow;
        public Action OpenHomeWindow = new Action(() => new HomeWindow().Show());
        private JumboServer _serverFunctions;
        public JumboServer ServerFunctions
        {
            get { return _serverFunctions; }
            private set { _serverFunctions = value; OnPropertyChanged(); }
        }
        public string LoginInput
        {
            get { return _loginInput; }
            set { _loginInput = value; OnPropertyChanged(); }
        }
        private string _loginInput;
        public string PasswordInput
        {
            get { return _passwordInput; }
            set { _passwordInput = value; OnPropertyChanged(); }
        }
        private string _passwordInput;
        public bool AuthResult
        { 
            get { return _authResult; }
            set { _authResult = value; OnPropertyChanged(); }
        }
        private bool _authResult = false;
        
        #endregion

        #region Commands
        public MyCommand ExecuteAuthorization
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    var passwordBox = (PasswordBox)obj;
                    PasswordInput = passwordBox.Password;
                    Notice = "";

                    var inputData = new Person(LoginInput, PasswordInput);
                    AuthResult = await JumboServer.ActiveServer.Authorization(inputData, true);
                    ShowAuthResult();
                    if (AuthResult)
                        GoToHomeWindow();
                }, (obj) => CheckInputCondition());
            }
        }
        public MyCommand AuthorizationWithToken
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    var token = ServerFunctions.DeserializeTokenByFileName("token-auth");
                    AuthResult = await ServerFunctions.AuthorizationByTokenAsync(token);
                    ShowAuthResult();
                    if (AuthResult)
                        GoToHomeWindow();
                });
            }
        }

        #endregion

        #region Methods
        private void GoToHomeWindow()
        {
            OpenHomeWindow();
            CloseAuthWindow();
        }
        private void ShowAuthResult()
        {
            if (AuthResult)
                Notice = "Успешная авторизация";
            else
                Notice = "Ошибка авторизации";
        }
        #endregion

        #region ValidationMethods
        public bool CheckInputCondition()
        {
            if(string.IsNullOrWhiteSpace(PasswordInput) &&
               string.IsNullOrWhiteSpace(LoginInput))
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
