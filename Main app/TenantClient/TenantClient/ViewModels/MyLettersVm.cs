using ExchangeSystem.v2.Entities;
using ExchangeSystem.v2.Packages;
using ExchangeSystem.v2.Packages.Default;
using ExchangeSystem.v2.Sendlers;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TenantClient.Commands;
using TenantClient.Local;

namespace TenantClient.ViewModels
{
    internal class MyLettersVm : BaseVM
    {
        public ObservableCollection<Letter> MyLetters
        {
            get => _myLetters;
            set
            {
                _myLetters = value;
                OnPropertyChanged("MyLetters");
            }
        }
        private ObservableCollection<Letter> _myLetters = new ObservableCollection<Letter>();
        private string _authToken;

        public MyCommand GetMyLetters
        {
            get => new MyCommand(async (obj) =>
            {
                var tokenWasExist = ClientTokenStorage.TryGet(out _authToken);
                if (tokenWasExist)
                {
                    ResetLetters();
                    await GetLettersFromServer();
                }
            });
        }
        private void ResetLetters()
        {
            MyLetters = new ObservableCollection<Letter>();
        }
        private async Task GetLettersFromServer()
        {
            var request = PrepareRequestPackage();
            var response = await SendRequest(request);
            MyLetters = EncryptResponseAsLetters(response);
        }
        private GetMyLetters PrepareRequestPackage()
        {
            var request = new GetMyLetters();
            request.SetToken(_authToken);
            return request;
        }
        private async Task<ResponsePackage> SendRequest(BaseRequestPackage requestPackage)
        {
            var sendler = new RequestSendler(new ConnectionSettings("127.0.0.1", 80));
            return await sendler.SendRequest(requestPackage);
        }
        private ObservableCollection<Letter> EncryptResponseAsLetters(ResponsePackage response)
        {
            var jLetters = response.ResponseData as JArray;
            var letters = jLetters.ToObject<ObservableCollection<Letter>>();
            return letters;
        }
    }
}
