using ExchangeSystem.v2.Entities;
using ExchangeSystem.v2.Packages;
using ExchangeSystem.v2.Packages.Default;
using ExchangeSystem.v2.Sendlers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantClient.Commands;
using TenantClient.Local;
using TenantClient.Pages;

namespace TenantClient.ViewModels
{
    internal class MyLettersVm : BaseVM
    {
        public List<Letter> MyLetters
        {
            get => _myLetters;
            set
            {
                _myLetters = value;
                OnPropertyChanged("MyLetters");
            }
        }
        private List<Letter> _myLetters = new List<Letter>();
        public Letter SelectedLetter
        {
            get => _selectedLetter;
            set
            {
                _selectedLetter = value;
                if(ReadLetterPage == null)
                    ReadLetterPage = new ReadLetter(SelectedLetter);
                SelectedLetterChanged?.Invoke(SelectedLetter);
                OnPropertyChanged("SelectedLetter");
            }
        }
        private Letter _selectedLetter;
        public ReadLetter ReadLetterPage
        {
            get => _readLetterPage;
            set
            {
                _readLetterPage = value;
                var vm = _readLetterPage.DataContext as ReadLetterVm;
                vm.SetActionWhenSelectedLetterChanged(ref SelectedLetterChanged);
                OnPropertyChanged("ReadLetterPage");
            }
        }
        private ReadLetter _readLetterPage;
        public event Action<Letter> SelectedLetterChanged;
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
            MyLetters = new List<Letter>();
        }
        private async Task GetLettersFromServer()
        {
            var request = PrepareRequestPackage();
            var response = await SendRequest(request);
            MyLetters = EncryptResponseAsLetters(response).OrderBy(letter => letter.DateCreate).ToList();
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
        private List<Letter> EncryptResponseAsLetters(ResponsePackage response)
        {
            var jLetters = response.ResponseData as JArray;
            var letters = jLetters.ToObject<List<Letter>>();
            return letters;
        }
    }
}
