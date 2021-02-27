using Multi_Server_Test.Server.Packages;
using Newtonsoft.Json;
using System;

namespace Multi_Server_Test.Server.Blocks.LetterBlock
{
    public class Letter : RequestObject
    {
        public string LetterType { get; }
        public string Title { get; }
        public string Description { get; }
        public string SenderLogin { get; }
        public DateTime DateCreate { get; }

        [JsonConstructor]
        public Letter(string title, string description, string senderLogin, string letterType, DateTime dateCreate)
        {
            Title = title;
            Description = description;
            SenderLogin = senderLogin;
            LetterType = letterType;
            if (dateCreate == null)
                DateCreate = DateTime.Now;
            else DateCreate = dateCreate;
        }
        public override string ToString()
        {
            return $"TYPE: {LetterType}\n" +
                   $"TITLE: {Title}\n" +
                   $"DESCRIPTION: {Description}\n" +
                   $"FROM: " + SenderLogin;
        }
    }
}
