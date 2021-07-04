using ExchangeSystem.v2.Entities;
using ExchangeSystem.v2.Packages.Default;
using ExchangeSystem.v2.Sendlers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TenantClient.Commands;
using TenantClient.Pages;

namespace TenantClient.ViewModels
{
    internal class ReadLetterVm : BaseVM
    {
        private int _letterId;
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
        public ObservableCollection<(Letter, ReadLetter)> Responses
        {
            get => _responses;
            set
            {
                OnPropertyChanged("Responses");
                _responses = value;
            }
        }
        private ObservableCollection<(Letter, ReadLetter)> _responses = new ObservableCollection<(Letter, ReadLetter)>();
        /// <summary>
        /// Отобразит письмо, id которого был передан в конструктор
        /// </summary>
        /// <param name="letterId">Идентификатор письма</param>
        public ReadLetterVm(int letterId)
        {
            throw new NotImplementedException("А че а почему");
            _letterId = letterId;
        }
        public ReadLetterVm(Letter letter)
        {
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
                await Task.Run(()=>
                {
                    GetResponsesAsync();
                });
            });
        }
        public async Task GetResponsesAsync()
        {
            var request = new GetLetterResponses(Letter.Id);
            var sendler = new RequestSendler(new ConnectionSettings());
            var response = await sendler.SendRequest(request);
            if(response.Status == ResponseStatus.Ok)
            {
                var jArrayResponses = response.ResponseData as JArray;
                var responses = jArrayResponses.ToObject<ObservableCollection<Letter>>();
            }
        }
        public void RetreiveLetter(Letter letter)
        {
            Letter = letter;
            GetResponsesById?.Execute(null);
        }
        public void SetActionWhenSelectedLetterChanged(ref Action<Letter> action)
        {
            action += (letter) => RetreiveLetter(letter);
        }
    }
}
