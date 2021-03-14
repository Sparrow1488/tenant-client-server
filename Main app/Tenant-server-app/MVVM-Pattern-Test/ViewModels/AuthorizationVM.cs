using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.Views;
using System;
using System.Threading;
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
        }
        #endregion

        #region AllProps
        public Action CloseAuthWindow;
        public Action OpenHomeWindow = new Action(() => new HomeWindow().Show());
        private JumboServer _serverFunctions;
        public JumboServer ServerFunctions
        {
            get { return _serverFunctions; }
            private set { _serverFunctions = value; OnPropertyChanged(); }
        }
        private string _loginInput;
        public string LoginInput
        {
            get { return _loginInput; }
            set { _loginInput = value; OnPropertyChanged(); }
        }
        private string _passwordInput;
        public string PasswordInput
        {
            get { return _passwordInput; }
            set { _passwordInput = value; OnPropertyChanged(); }
        }
        private string _infoMessage;

        private bool _authResult = false;
        public bool AuthResult
        { 
            get { return _authResult; }
            set { _authResult = value; OnPropertyChanged(); }
        }
        public string InfoMessage 
        {
            get { return _infoMessage; }
            set { _infoMessage = value; OnPropertyChanged(); }
        }
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
                    InfoMessage = "";

                    AuthResult = await Task.Factory.StartNew(new Func<bool>(() =>
                    {
                        var inputData = new Person(LoginInput, PasswordInput);
                        AuthResult = JumboServer.ActiveServer.Authorization(inputData, true);
                        if (AuthResult)
                        {
                            InfoMessage = "Успешная авторизация";
                            return true;
                        }
                        else
                            InfoMessage = "Ошибка авторизации";
                        return false;
                    }));
                    if (AuthResult)
                    {
                        OpenHomeWindow();
                        CloseAuthWindow();
                    }
                });
            }
        }
        #endregion

        #region Methods

        #endregion

        #region ValidationMethods
        public bool CheckInputCondition()
        {
            if(string.IsNullOrWhiteSpace(_loginInput) &&
               string.IsNullOrWhiteSpace(_passwordInput))
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
