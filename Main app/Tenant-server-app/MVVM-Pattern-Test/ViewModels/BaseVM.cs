using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM_Pattern_Test.ViewModels
{
    public abstract class BaseVM : INotifyPropertyChanged
    {
        public abstract string Notice { get; protected set; }
        public string _infoMessage;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
