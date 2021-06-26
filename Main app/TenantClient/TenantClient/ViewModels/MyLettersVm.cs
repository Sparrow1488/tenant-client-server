using ExchangeSystem.v2.Entities;
using ExchangeSystem.v2.Packages.Default;
using ExchangeSystem.v2.Sendlers;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
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
                    var request = new GetMyLetters();
                    request.SetToken(_authToken);
                    var sendler = new RequestSendler(new ConnectionSettings("127.0.0.1", 80));
                    var response = await sendler.SendRequest(request);
                    var jLetters = response.ResponseData as JArray;
                    var letters = jLetters.ToObject<Letter[]>();
                    foreach (var letter in letters)
                        MyLetters.Add(letter);
                }
            });
        }
    }
}
