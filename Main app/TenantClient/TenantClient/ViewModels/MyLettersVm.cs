using ExchangeSystem.v2.Entities;
using System.Collections.ObjectModel;
using TenantClient.Commands;

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

        public MyCommand GetMyLetters
        {
            get => new MyCommand(async (obj) =>
            {

            });
        }
    }
}
