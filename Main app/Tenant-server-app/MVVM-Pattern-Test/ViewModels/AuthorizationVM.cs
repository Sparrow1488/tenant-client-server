using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages.Default;
using MVVM_Pattern_Test.ClientEntities;
using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.Views;
using Newtonsoft.Json;
using System;
using System.Windows.Controls;

namespace MVVM_Pattern_Test.ViewModels
{
    public class AuthorizationVM : BaseVM
    {
        #region Constructor
        public AuthorizationVM()
        {
            LoginInput = "";
            PasswordInput = "";

            SetActionForOpenWindow();
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
        public Action OpenHomeWindow;
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
        public User AuthUser { get; private set; }
        
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
                    var inputValidData = CheckInputCondition();
                    if (!inputValidData)
                        ShowAuthResult("Введены не корректно логин или пароль");
                    else
                    {
                        Notice = "Вход в систему...";
                        var manager = new ExSysManager();
                        var response = await manager.LogIn(LoginInput, PasswordInput, true);
                        if (response != null && response.Status == ResponseStatus.Ok)
                        {
                            AuthUser = response.ResponseData as User;
                            new FilesHelper().SaveTokenLocal(AuthUser.Passport.Token);
                            ShowAuthResult("Успешный вход в систему");
                            GoToHomeWindow();
                        }
                        ShowAuthResult($"Возникла ошибка при авторизации в систему: {response?.ErrorMessage}");
                    }
                }, (obj) => CheckInputCondition());
            }
        }
        public MyCommand AuthorizationWithToken
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    var token = new FilesHelper().OpenTokenLocal();
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        var manager = new ExSysManager();
                        var response = await manager.TokenLogIn(token);
                        if(response.Status == ResponseStatus.Ok)
                        {
                            AuthUser = response.ResponseData as User;
                            ShowAuthResult("Успешный вход в систему");
                            GoToHomeWindow();
                        }
                    }
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
        private void ShowAuthResult(string message)
        {
            Notice = message;
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
        private void SetActionForOpenWindow()
        {
            OpenHomeWindow = () => new HomeWindow(AuthUser).Show();
        }
        #endregion
    }
}
