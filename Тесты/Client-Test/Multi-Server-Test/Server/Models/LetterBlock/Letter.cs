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
        public Letter(string title, string description, string sender, string letterType, DateTime dateCreate)
        {
            Title = title;
            Description = description;
            SenderLogin = sender;
            LetterType = letterType;
            if (dateCreate == null)
                DateCreate = DateTime.Now;
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
