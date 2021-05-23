using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages.Default;
using MVVM_Pattern_Test.ClientEntities;
using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.Views;
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
                    Notice = "Отправка запроса на вход в систему...";

                    var manager = new ExSysManager();
                    var response = await manager.Authorization(LoginInput, PasswordInput, true);
                    if (response.Status == ResponseStatus.Ok)
                    {
                        AuthUser = response.ResponseData as User;
                        ShowAuthResult();
                        GoToHomeWindow();
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

                });
            }
        }

        #endregion

        #region Methods
        //private async Task TryAuthForToken(UserToken token)
        //{
        //    //Notice = "Вход в систему...";
        //    try
        //    {
        //        AuthResult = await ServerFunctions.AuthorizationByTokenAsync(token);
        //        ShowAuthResult();
        //        if (AuthResult)
        //            GoToHomeWindow();
        //    }
        //    catch (JumboServerException ex) { Notice = ex.Message; }
        //    catch (Exception) { }
        //}
        //private async Task TryAuth(Person inputData)
        //{
        //    Notice = "Вход в систему...";
        //    try
        //    {
        //        AuthResult = await ServerFunctions.AuthorizationAsync(inputData, true);
        //        ShowAuthResult();
        //        if (AuthResult)
        //            GoToHomeWindow();
        //    }
        //    catch (JumboServerException ex) { Notice = ex.Message; }
        //    catch (Exception) { }
        //}
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
        private void SetActionForOpenWindow()
        {
            OpenHomeWindow = () => new HomeWindow(AuthUser).Show();
        }
        #endregion
    }
}
