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
        public string[] SourcesTokens { get; }
        public int SenderId { get; }
        public DateTime DateCreate { get; }
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
