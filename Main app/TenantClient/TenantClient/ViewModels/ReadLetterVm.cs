using ExchangeSystem.v2.Entities;
using System;
using TenantClient.Commands;

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
        /// <summary>
        /// Отобразит письмо, id которого был передан в конструктор
        /// </summary>
        /// <param name="letterId">Идентификатор письма</param>
        public ReadLetterVm(int letterId)
        {
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
        public void RetreiveLetter(Letter letter)
        {
            Letter = letter;
        }
        public void SetActionWhenSelectedLetterChanged(ref Action<Letter> action)
        {
            action += (letter) => RetreiveLetter(letter);
        }
    }
}
