using MVVM_Pattern_Test.Commands;
using MVVM_Pattern_Test.ViewModels;
using System.Collections.Generic;

namespace MVVM_Pattern_Test
{
    public class ApplicationViewModel : BaseVM
    {
        private Gun gun;
        private int _count = 0;

        public List<Gun> Guns { get; set; }
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
        public MyCommand EditGunModel
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    SelectGun.Model = (string)obj;
                }, (obj) => Count < 10);
            }
        }
        public MyCommand EditGunCaliber
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    SelectGun.Caliber = (string)obj;
                });
            }
        }
        public MyCommand EditGunYearCreate
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    SelectGun.YearCreate = (string)obj;
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
            Guns = new List<Gun>();

            Guns.Add(new Gun() { Model = "AR-15", Caliber = ".223 REM", YearCreate = "1950" });
            Guns.Add(new Gun() { Model = "AK-74", Caliber = "5.45x39", YearCreate = "неважно" });
            Guns.Add(new Gun() { Model = "HK-416", Caliber = ".223", YearCreate = "неважно" });
            Guns.Add(new Gun() { Model = "MP-133", Caliber = "12/70", YearCreate = "неважно" });
        }

    }
}
