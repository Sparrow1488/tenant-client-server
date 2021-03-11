using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM_Pattern_Test
{
    // МОДЕЛЬ
    public class Gun : INotifyPropertyChanged
    {
        private string model;
        private string caliber;
        private string yearCreate;
        public string Model { get { return model; } set { model = value; OnPropertyChanged(); } }
        public string Caliber { get { return caliber; } set { caliber = value; OnPropertyChanged(); } }
        public string YearCreate { get { return yearCreate; } set { yearCreate = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
