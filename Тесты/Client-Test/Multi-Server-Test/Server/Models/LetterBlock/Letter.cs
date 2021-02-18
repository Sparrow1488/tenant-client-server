using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Packages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Blocks.LetterBlock
{
    public class Letter : RequestObject
    {
        public string LetterType { get; }
        public string Title { get; }
        public string Description { get; }
        public Person Sender { get; }

        [JsonConstructor]
        public Letter(string title, string description, Person sender, string letterType)
        {
            Title = title;
            Description = description;
            Sender = sender;
            LetterType = letterType;
        }
        public override string ToString()
        {
            return $"TYPE: {LetterType}\n" +
                   $"TITLE: {Title}\n" +
                   $"DESCRIPTION: {Description}\n" +
                   $"FROM: " +
                   $"{Sender.LastName} " +
                   $"{Sender.Name} " +
                   $"{Sender.ParentName}" +
                   $"({Sender.Login})" +
                   $" - {Sender.Room}";
        }
    }
}
