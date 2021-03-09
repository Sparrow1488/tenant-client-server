using Newtonsoft.Json;
using System;

namespace WpfApp1.Server.Packages.Letters
{
    public class Letter : RequestObject
    {
        public string LetterType { get; protected set; }
        public int Id { get; }
        public string Title { get; }
        public string Description { get; }
        public string SenderLogin { get; }
        public int SenderId { get; }
        public DateTime DateCreate = DateTime.Now;
        public Letter(string title, string description, int senderId)
        {
            if(senderId > -1)
            {
                Title = title;
                Description = description;
                SenderId = senderId;
            }
        }
        [JsonConstructor]
        public Letter(string title, string description, string senderLogin, string letterType, DateTime dateCreate, int id, int senderId)
        {
            if (!string.IsNullOrWhiteSpace(senderLogin))
            {
                Title = title;
                Description = description;
                SenderLogin = senderLogin;
                LetterType = letterType;
                DateCreate = dateCreate;
                Id = id;
                SenderId = senderId;
            }
        }
    }
}
