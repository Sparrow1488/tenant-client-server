using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName;
        public string SecondName;
        public int DoorNumber;

        public string Login = null;
        public string Password = null;

        public static User Active = null;

        public User(string fName, string lName, string secName, int numRoom)
        {
            if (!string.IsNullOrWhiteSpace(fName) &&
                !string.IsNullOrWhiteSpace(lName) &&
                (numRoom > 0 && numRoom < 100))
            {
                FirstName = fName;
                LastName = lName;
                SecondName = secName;
                DoorNumber = numRoom;
            }
            else
            {
                throw new ArgumentException("Вы пытаетесь создать пустой пакет регистрации");
            }
            Active = this;
        }
    }
}
