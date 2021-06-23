using ExchangeSystem.v2.Entities;
using ExchangeSystem.v2.Packages;
using ExchangeSystem.v2.Packages.Default;
using ExchangeSystem.v2.Sendlers;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using TenantClient.Commands;
using TenantClient.Exceptions;
using TenantClient.Local;
using TenantClient.Views;

namespace TenantClient.ViewModels
{
    internal class AuthVm : BaseVM
    {
        public string InputLogin { get; set; }
        public string InputPassword { get; set; }
        private Action OpenMainWindow { get; set; } = () => new HomeWindow().Show();
        private Action CloseAuthWindow { get; set; }
        public void SetCloseAuthWindowAction(Action action)
        {
            if (action != null)
                CloseAuthWindow = action;
            else
                throw new ArgumentNullException($"{nameof(action)} был null. Невозможно присвоить значение для {nameof(CloseAuthWindow)}");
        }
        public MyCommand LogIn
        {
            get
            {
                return new MyCommand(async (obj) =>
                {
                    var passwordBox = obj as PasswordBox;
                    InputPassword = passwordBox.Password;
                    NoticeMessage = "Вход в систему...";
                    if (IsInputDataValid())
                    {
                        var authPack = new Authorization(new UserPassport(InputLogin, InputPassword));
                        var response = await SendRequest(authPack);
                        if (SuccessAuthorization(response))
                        {
                            await ClientTokenStorage.SaveOnMachineAsync(response.ResponseData as string, true);
                            CompleteEntrance();
                        }
                    }
                    else
                        NoticeMessage = "Вы не ввели логин или пароль";
                });
            }
        }
        public MyCommand LogInToken
        {
            get => new MyCommand(async (obj)=>
            {
                Task.Delay(1000).Wait();
                var success = ClientTokenStorage.TryGet(out string clientToken);
                if (success)
                {
                    var tokenAuthPack = new TokenAuthorization(clientToken);
                    var response = await SendRequest(tokenAuthPack);
                    if (SuccessAuthorization(response))
                        CompleteEntrance();
                }
            }, (obj) => ClientTokenStorage.TokenWasExist());
        }
        private async Task<ResponsePackage> SendRequest(BaseRequestPackage package)
        {
            var sender = new RequestSendler(new ConnectionSettings("127.0.0.1", 80));
            var response = await sender.SendRequest(package);
            return response;
        }
        private bool IsInputDataValid()
        {
            if (!string.IsNullOrWhiteSpace(InputLogin) && !string.IsNullOrWhiteSpace(InputPassword))
                return true;
            return false;
        }
        private void CompleteEntrance()
        {
            if (OpenMainWindow == null)
                throw new WindowException("Не удалось открыть окно программы (HomeWindow)");
            if (CloseAuthWindow == null)
                throw new WindowException("Не удалось закрыть окно авторизации (AuthWindow)");
            else
            {
                OpenMainWindow?.Invoke();
                CloseAuthWindow?.Invoke();
            }
        }
        private bool SuccessAuthorization(ResponsePackage response)
        {
            if (response.Status == ResponseStatus.Ok)
            {
                NoticeMessage = "Успешный вход";
                return true;
            }
            NoticeMessage = "Ошибка авторизации";
            return false;
        }
    }
}
