using Newtonsoft.Json;
using System;
using WpfApp1.Server.Packages;

namespace WpfApp1.Server
{
    public class Person : RequestObject
    {
        public string Name { get; }
        public string LastName { get; }
        public string ParentName { get; }
        public string Login { get; }
        public string Password { get; }
        public int Room { get; }
        public int ID { get; }

        [JsonConstructor]
        public Person(string login, string name, string lastName, string parentName, int room, int id, string password)
        {
            var validName = CheckInputValidation(name, lastName, parentName);
            var validAccountInfo = CheckInputValidation(login, password, room);

            if (validName && validAccountInfo)
            {
                Login = login;
                Name = name;
                LastName = lastName;
                ParentName = parentName;
                Room = room;
                ID = id;
                Password = password;
            }
            else
            {
                throw new ArgumentException("Вы ввели некорректные данные");
            }
        }

        public Person(string login, string password, int room)
        {
            var validAccount = CheckInputValidation(login, password, room);
            if (validAccount)
            {
                Login = login;
                Password = password;
                Room = room;
                //TODO: наверняка это присвоение можно как то упростить
            }
            else
            {
                throw new ArgumentException("Вы ввели некорректные данные");
            }
        }

        private bool CheckInputValidation(string name, string lastName, string parentName)
        {
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(parentName))
                return false;
            else
                return true;
        }
        private bool CheckInputValidation(string login, string password, int room)
        {
            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password) ||
                room <= 0)
                return false;
            else
                return true;
        }
        //private bool CheckInputValidation(string login, int room)
        //{
        //    if (string.IsNullOrWhiteSpace(login) ||
        //        room <= 0)
        //        return false;
        //    else
        //        return true;
        //}

    }
}
