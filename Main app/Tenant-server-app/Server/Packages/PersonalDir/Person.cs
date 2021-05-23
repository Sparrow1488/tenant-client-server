using Newtonsoft.Json;
using System;
using WpfApp1.Server.Packages;
using WpfApp1.Server.Packages.PersonalDir;

namespace WpfApp1.Server
{
    public class Person : RequestObject
    {
        //TODO: токен авторизованных пользователей (объект токена)
        public string Name { get; }
        public string LastName { get; }
        public string ParentName { get; }
        public string Login { get; }
        public int Id { get; }
        public string Password { get; }
        public int Room { get; }
        public UserToken Token { get; set; }
        public int AdminStatus { get; }

        [JsonConstructor]
        public Person(string login, string name, string lastName, string parentName, int room, string password, int id, UserToken token, int adminStatus)
        {
            Login = login;
            Name = name;
            LastName = lastName;
            ParentName = parentName;
            Room = room;
            Password = password;
            Id = id;
            Token = token;
            AdminStatus = adminStatus;
        }

        public Person(string login, string password)
        {
            var validAccount = CheckInputValidation(login, password);
            if (validAccount)
            {
                Login = login;
                Password = password;
            }
        }
        private bool CheckInputValidation(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password))
                return false;
            else
                return true;
        }
    }
}
