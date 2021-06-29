using ExchangeSystem.v2.Entities;
using ExchangeSystem.v2.Packages;
using ExchangeSystem.v2.Packages.Default;
using ExchangeSystem.v2.Sendlers;
using System;
using System.Threading.Tasks;
using TenantClient.Commands;
using TenantClient.Exceptions;
using TenantClient.Local;

namespace TenantClient.ViewModels
{
    internal class ProfileVm : BaseVM
    {
        private int _accountId;
        private string _userToken;
        public UserInfo Account
        {
            get => _account;
            set 
            {
                _account = value;
                OnPropertyChanged("Account");
            } 
        }
        private UserInfo _account;
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged("FullName");
            }
        }
        private string _fullName;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }
        private string _login;
        /// <summary>
        /// Отобразить страничку определенного пользователя
        /// </summary>
        public ProfileVm(int accountId)
        {
            _accountId = accountId;
        }
        /// <summary>
        /// Отобразить свою страничку, используя токен авторизации
        /// </summary>
        public ProfileVm()
        {
            var success = ClientTokenStorage.TryGet(out _userToken);
            if (!success)
                throw new ShowProfileException("Не удалось отобразить профиль из-за отсутствия токена авторизации!");
        }
        public MyCommand ShowAccount
        {
            get => new MyCommand(async (obj)=>
            {
                await ReceiveMyAccountFromServer();
            });
        }
        private async Task ReceiveMyAccountFromServer()
        {
            var receiveAccountRequest = new GetMyAccount();
            receiveAccountRequest.SetToken(_userToken);
            var response = await SendRequest(receiveAccountRequest);
            if (response.Status == ResponseStatus.Ok)
                Account = response.ResponseData as UserInfo;
            FullName = $"{Account?.LastName} {Account?.Name} {Account?.ParentName}";
            Login = Account?.login;
        }
        private async Task<ResponsePackage> SendRequest(BaseRequestPackage package)
        {
            var sender = new RequestSendler(new ConnectionSettings("127.0.0.1", 80));
            var response = await sender.SendRequest(package);
            return response;
        }
    }
}
