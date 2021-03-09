using Multi_Server_Test.Server.Packages;
using Newtonsoft.Json;
using System;

namespace Multi_Server_Test.Server.Blocks.LetterBlock
{
    public class Letter : RequestObject
    {
        public string LetterType { get; }
        public string Title { get; }
        public int Id { get; }
        public string Description { get; }
        public string SenderLogin { get; }
        public int SenderId { get; }
        public DateTime DateCreate { get; }

        [JsonConstructor]
        public Letter(string title, string description, string senderLogin, string letterType, DateTime dateCreate, int id, int senderId)
        {
            Title = title;
            Description = description;
            SenderLogin = senderLogin;
            LetterType = letterType;
            if (dateCreate == null)
                DateCreate = DateTime.Now;
            else DateCreate = dateCreate;
            Id = id;
            SenderId = senderId;
        }
        public override string ToString()
        {
            return $"TYPE: {LetterType}\n" +
                   $"TITLE: {Title}\n" +
                   $"FROM: " + SenderLogin;
        }
    }
}
