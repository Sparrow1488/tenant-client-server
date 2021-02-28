using Newtonsoft.Json;
using System;

namespace WpfApp1.Server.Packages.Letters
{
    public class Letter : RequestObject
    {
        public string LetterType { get; protected set; }
        public string Title { get; }
        public string Description { get; }
        public string SenderLogin { get; }
        public DateTime DateCreate = DateTime.Now;
        public Letter(string title, string description, string senderLogin)
        {
            if(CheckValidation(title, description, senderLogin))
            {
                Title = title;
                Description = description;
                SenderLogin = senderLogin;
            }
        }
        [JsonConstructor]
        public Letter(string title, string description, string senderLogin, string letterType, DateTime dateCreate)
        {
            if (CheckValidation(title, description, senderLogin))
            {
                Title = title;
                Description = description;
                SenderLogin = senderLogin;
                LetterType = letterType;
                DateCreate = dateCreate;
            }
        }
        private bool CheckValidation(string title, string desc, string senderLogin)
        {
            if (string.IsNullOrWhiteSpace(desc) || senderLogin == null || string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Вы не можете отправить пустое письмо");
            return true;
        }
    }
}
