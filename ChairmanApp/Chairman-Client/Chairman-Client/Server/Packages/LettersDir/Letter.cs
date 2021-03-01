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
            if(!string.IsNullOrWhiteSpace(senderLogin))
            {
                Title = title;
                Description = description;
                SenderLogin = senderLogin;
            }
        }
        [JsonConstructor]
        public Letter(string title, string description, string senderLogin, string letterType, DateTime dateCreate)
        {
            if (!string.IsNullOrWhiteSpace(senderLogin))
            {
                Title = title;
                Description = description;
                SenderLogin = senderLogin;
                LetterType = letterType;
                DateCreate = dateCreate;
            }
        }
    }
}
