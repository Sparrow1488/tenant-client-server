using ExchangeSystem.v2.Entities;
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
            Letter = letter;
        }

        public MyCommand GetLetterById
        {
            get => new MyCommand(async (obj) =>
            {

            });
        }
    }
}
