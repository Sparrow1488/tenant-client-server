using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TenantClient.ViewModels
{
    internal abstract class BaseVM : INotifyPropertyChanged
    {
        public string NoticeMessage
        {
            get { return _infoMessage; }
            protected set { _infoMessage = value; OnPropertyChanged(); }
        }
        private string _infoMessage;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
