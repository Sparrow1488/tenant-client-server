using MVVM_Pattern_Test.MyApplication.UserInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Server;
using WpfApp1.Server.ServerMeta;

namespace MVVM_Pattern_Test.ViewModels
{
    public class ProfileVM : BaseVM
    {
        public ProfileVM()
        {
            User.Info = JumboServer.ActiveServer.ActiveUser;
            AuthUser = User.Info;
        }

        public override string Notice
        {
            get { return _infoMessage; }
            protected set { _infoMessage = value; OnPropertyChanged(); }
        }

        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(); }
        }
        public string _login;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public string _name;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(); }
        }
        public string _lastName;
        public string ParentName
        {
            get { return _parentName; }
            set { _parentName = value; OnPropertyChanged(); }
        }
        public string _parentName;
        public string FullName
        {
            get { return $"{AuthUser.LastName} {AuthUser.Name} {AuthUser.ParentName}"; }
            set { _parentName = value; OnPropertyChanged(); }
        }
        public Person AuthUser
        {
            get { return _authUser; }
            private set { _authUser = value; OnPropertyChanged(); }
        }
        private Person _authUser;
    }
}
