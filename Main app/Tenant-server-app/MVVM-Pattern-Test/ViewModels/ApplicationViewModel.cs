using MVVM_Pattern_Test.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Pattern_Test
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Gun gun;
        private int _count = 0;
        public List<Gun> Guns = new List<Gun>();
        public int Count 
        { 
            get 
            { return _count; } 
            set 
            { 
                _count = value;
                OnPropertyChanged(); 
            } 
        }
        public MyCommand Click
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    Count++;
                }, (obj) => Count < 10);
            }
        }
        public MyCommand RenameGun
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    SelectGun.Model = (string)obj;
                }, (obj) => Count < 10);
            }
        }
        public Gun SelectGun
        {
            get { return gun; }
            set { gun = value; OnPropertyChanged(); }
        } 
        public ApplicationViewModel()
        {
            SelectGun = new Gun() { Model="AR-15", Caliber=".222 REM", YearCreate="1950"};
            Guns.Add(SelectGun);
            Guns.Add(new Gun() { Model = "AK-74", Caliber = "5.45x39", YearCreate = "неважно" });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
