using Newtonsoft.Json;
using WpfApp1.Server.Packages;

namespace Chairman_Client.Server.Packages.LettersDir
{
    public class ReplyLetter : RequestObject
    {
        public string Answer { get; }
        public string Source { get; }
        public string Responder { get; }
        public int LetterId { get; }
        [JsonConstructor]
        public ReplyLetter(string answer, string source, string responder, int letterId)
        {
            Answer = answer;
            Source = source;
            Responder = responder;
            LetterId = letterId;
        }
    }
}
