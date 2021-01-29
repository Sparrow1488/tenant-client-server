using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Blocks;
using WpfApp1.Server.Packages;

namespace WpfApp1.Classes
{
    public class Person : RequestObject
    {
        public Person(string firstN, string lastN, string secN, int roomNum, string pass)
        {
            var validName = CheckValidationFullName(firstN, lastN, secN);
            var validOther = CheckValidationOtherInfo(pass, roomNum);
            if (validName.Equals(true) && validOther.Equals(true))
            {
                Name = firstN;
                LastName = lastN;
                ParentName = secN;
                Room = roomNum;
                Password = pass;
            }
            else
            {
                throw new ArgumentException("Вы ввели некорректные данные");
            }
        }
        public Person(string login, string password, int room)
        {
            var validAccount = CheckValidationAccount(login, password, room);
            if (validAccount)
            {
                Login = login;
                Password = password;
                Room = room;
            }
            else
            {
                throw new ArgumentException("Вы ввели некорректные данные");
            }
        }
        public string Name { get; }
        public string LastName { get; }
        public string ParentName { get; }
        public string Login { get; }
        public string Password { get; }
        public int Room { get; }
        public int ID { get; }

        private bool CheckValidationFullName(string first, string last, string second)
        {
            if (string.IsNullOrWhiteSpace(first) ||
                string.IsNullOrWhiteSpace(last) ||
                string.IsNullOrWhiteSpace(second))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool CheckValidationOtherInfo(string pass, int roomNum)
        {
            if(string.IsNullOrWhiteSpace(pass) || roomNum <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool CheckValidationAccount(string login, string pass, int roomNum)
        {
            var validOther = CheckValidationOtherInfo(pass, roomNum);
            if(string.IsNullOrWhiteSpace(login) || validOther.Equals(false))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
