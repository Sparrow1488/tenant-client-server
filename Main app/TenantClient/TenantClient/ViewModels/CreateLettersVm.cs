using ExchangeSystem.v2.Entities;
using ExchangeSystem.v2.Packages.Default;
using ExchangeSystem.v2.Sendlers;
using System.Threading.Tasks;
using TenantClient.Commands;
using TenantClient.Exceptions;
using TenantClient.Local;

namespace TenantClient.ViewModels
{
    internal class CreateLettersVm : BaseVM
    {
        public Letter EditLetter
        {
            get => _editLetter;
            set
            {
                _editLetter = value;
                OnPropertyChanged("NewLetter");
            }
        }
        private Letter _editLetter = new Letter() { To = 3 };
        private string _userToken = string.Empty;

        public MyCommand SendLetter
        {
            get => new MyCommand(async (obj) =>
            {
                ResponsePackage response;
                if (EditLetterIsValid() && UserWasAuthorizate())
                {
                    response = await SendRequest();
                    if (SuccessInsert(response))
                        ResetEditLetter();
                    else
                        ShowErrorMessage(response);
                }
                else
                    ShowErrorMessage("Вы не можете отправить письмо, так как в письме не указан заголовок или основной текст обращения");
            });
        }

        private bool EditLetterIsValid()
        {
            if (EditLetter == null && string.IsNullOrWhiteSpace(EditLetter.Text) && string.IsNullOrWhiteSpace(EditLetter.Title))
                return false;
            return true;
        }
        private bool UserWasAuthorizate()
        {
            var userWasAuth = ClientTokenStorage.TryGet(out _userToken); 
            if (userWasAuth)
                return true;
            NoticeMessage = "Вы не можете отправлять письма, поскольку вы не вошли в систему";
            return false;
        }
        /// <summary>
        /// Отправляет запрос на сервер
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TokenException">Не удалось получить токен из локального хранилища</exception>
        private async Task<ResponsePackage> SendRequest()
        {
            var package = new AddLetter(EditLetter);
            package.SetToken(_userToken);
            var sendler = new RequestSendler(new ConnectionSettings("127.0.0.1", 80));
            var response = await sendler.SendRequest(package);
            return response;
        }

        private void ResetEditLetter()
        {
            EditLetter = new Letter() { To = 3 };
        }

        private bool SuccessInsert(ResponsePackage response)
        {
            if (response.Status == ResponseStatus.Ok)
            {
                NoticeMessage = response.ResponseData as string;
                return true;
            }
            return false;
        }

        private void ShowErrorMessage(ResponsePackage response)
        {
            NoticeMessage = response.ErrorMessage;
        }
        private void ShowErrorMessage(string message)
        {
            NoticeMessage = message;
        }

    }
}
