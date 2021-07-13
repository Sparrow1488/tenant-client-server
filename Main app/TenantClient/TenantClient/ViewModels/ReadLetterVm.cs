using ExchangeSystem.v2.Entities;
using ExchangeSystem.v2.Packages.Default;
using ExchangeSystem.v2.Sendlers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TenantClient.AdditionalStructs;
using TenantClient.Commands;

namespace TenantClient.ViewModels
{
    internal class ReadLetterVm : BaseVM
    {
        public Letter Letter
        {
            get => _letter;
            set
            {
                _letter = value;
                OnPropertyChanged("Letter");
            }
        }
        private Letter _letter;
        public string Sendler
        {
            get => Letter.sendler;
            set
            {
                OnPropertyChanged("Letter");
            }
        }
        public ObservableCollection<ReadLetterStruct> Responses
        {
            get => _responses;
            set
            {
                OnPropertyChanged("Responses");
                _responses = value;
            }
        }
        private ObservableCollection<ReadLetterStruct> _responses = new ObservableCollection<ReadLetterStruct>();
        public ReadLetterVm(Letter letter)
        {
            if (letter != null)
                RetreiveLetter(letter);
        }

        public MyCommand GetLetterById
        {
            get => new MyCommand((obj) =>
            {
                throw new NotImplementedException("Данный функционал еще не введен");
            });
        }
        public MyCommand GetResponsesById
        {
            get => new MyCommand(async (obj)=>
            {
                await Task.Run(() =>
                {
                    GetLetterResponsesAsync();
                });
            });
        }
        public MyCommand SelectResponse
        {
            get => new MyCommand((obj) =>
            {
                var response = obj as Letter;
                RetreiveLetter(response);
            });
        }
        public async Task GetLetterResponsesAsync()
        {
            var request = new GetLetterResponses(Letter.Id);
            var sendler = new RequestSendler(new ConnectionSettings());
            var response = await sendler.SendRequest(request);
            if (response.Status == ResponseStatus.Ok)
            {
                var jArrayResponses = response.ResponseData as JArray;
                var responses = jArrayResponses.ToObject<Letter[]>().OrderBy(letter => letter.DateCreate);

                ObservableCollection<ReadLetterStruct> test = new ObservableCollection<ReadLetterStruct>();
                foreach (var item in responses)
                {
                    var struct1 = new ReadLetterStruct(item);
                    test.Add(struct1);
                }
                Responses = test;
            }
            else
                Responses = new ObservableCollection<ReadLetterStruct>();
        }
        private void RetreiveLetter(Letter letter)
        {
            if(letter != null)
            {
                NoticeMessage = "Загружаю...";
                Letter = letter;
                GetResponsesById?.Execute("");
            }
        }
        public void SetActionWhenSelectedLetterChanged(ref Action<Letter> action)
        {
            action += (letter) => RetreiveLetter(letter);
        }
    }
}
