using Multi_Server_Test.Server.Packages;
using Newtonsoft.Json;

namespace Multi_Server_Test.Server.Blocks.LetterBlock
{
    public class Letter : RequestObject
    {
        public string LetterType { get; }
        public string Title { get; }
        public string Description { get; }
        public string SenderLogin { get; }

        [JsonConstructor]
        public Letter(string title, string description, string sender, string letterType)
        {
            Title = title;
            Description = description;
            SenderLogin = sender;
            LetterType = letterType;
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
