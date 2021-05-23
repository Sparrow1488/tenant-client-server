using ExchangeSystem.Requests.Objects.Entities;
using MVVM_Pattern_Test.Commands;

namespace MVVM_Pattern_Test.ViewModels
{
    public class ProfileVM : BaseVM
    {
        public ProfileVM(User user)
        {
            if(user != null)
            {
                UserProfile = user;
                FullName = string.Format("{0} {1} {2}", UserProfile.LastName, UserProfile.Name, UserProfile.ParentName);
                Name = UserProfile.Name;
            }
            Notice = "Пользователь не найден";
        }

        #region 
        public override string Notice
        {
            get { return _infoMessage; }
            protected set { _infoMessage = value; OnPropertyChanged(); }
        }
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; OnPropertyChanged(); }
        }
        private string _fullName;
        public int? Room
        {
            get { return _room; }
            set { _room = value; OnPropertyChanged(); }
        }
        private int? _room;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        private string _name;
        public User UserProfile
        {
            get { return _userProfile; }
            set { _userProfile = value; OnPropertyChanged(); }
        }
        private User _userProfile;
        #endregion

        public MyCommand ShowInfo
        {
            get
            {
                return new MyCommand((obj) =>
                {
                });
            }
        }

    }
}
