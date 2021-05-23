using Newtonsoft.Json;
using System;

namespace WpfApp1.Server.Packages.Letters
{
    public class Letter : RequestObject
    {
        public string LetterType { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SenderLogin { get; set; }
        public string[] SourcesTokens { get; set; }
        public int SenderId { get; set; }
        public DateTime DateCreate { get; set; }
        public string ShortDateCreate
        {
            get { return DateCreate.ToShortDateString(); }
            set { _shortDateString = value; }
        }
        private string _shortDateString;
        public Letter(string title, string description, int senderId, int letterType)
        {
            Title = title;
            Description = description;
            SenderId = senderId;
            DateCreate = DateTime.Now;
            LetterType = "testingTestType";
        }
        public Letter(string title, string description, int senderId, string[] sourcesTokens, int letterType)
        {
            
            Title = title;
            Description = description;
            SenderId = senderId;
            SourcesTokens = sourcesTokens;
            DateCreate = DateTime.Now;
            LetterType = "testingTestType";
        }
        public Letter(int id)
        {
            if (id > -1)
                Id = id;
        }
        [JsonConstructor]
        public Letter(string title, string description, string senderLogin, string letterType, DateTime dateCreate, int id, int senderId, string[] sourcesTokens)
        {
            Title = title;
            Description = description;
            SenderLogin = senderLogin;
            LetterType = letterType;
            DateCreate = dateCreate;
            Id = id;
            SenderId = senderId;
            SourcesTokens = sourcesTokens;
        }
    }
}
