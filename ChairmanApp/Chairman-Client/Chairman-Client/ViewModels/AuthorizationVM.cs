using Chairman_Client;
using MVVM_Pattern_Test.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Server;
using WpfApp1.Server.Packages.PersonalDir;
using WpfApp1.Server.ServerExceptions;
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
                    Notice = "Отправка запроса на вход в систему...";

                    var inputData = new Person(LoginInput, PasswordInput);
                    await TryAuth(inputData);
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
                    await TryAuthForToken(token);
                });
            }
        }

        #endregion

        #region Methods
        private async Task TryAuthForToken(UserToken token)
        {
            Notice = "Вход в систему...";
            try
            {
                AuthResult = await ServerFunctions.AuthorizationByTokenAsync(token);
                ShowAuthResult();
                if (AuthResult)
                    GoToHomeWindow();
            }
            catch (JumboServerException ex) { Notice = ex.Message; }
            catch (Exception ex) { Notice = ex.Message; }
        }
        private async Task TryAuth(Person inputData)
        {
            Notice = "Вход в систему...";
            try
            {
                AuthResult = await ServerFunctions.Authorization(inputData, true);
                ShowAuthResult();
                if (AuthResult)
                    GoToHomeWindow();
            }
            catch (JumboServerException ex) { Notice = ex.Message; }
        }
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
                Notice = "Не удалось войти в систему. Проверьте правильность логина или пароля";
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
