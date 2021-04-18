using Newtonsoft.Json;
using WpfApp1.Server.Packages;

namespace Chairman_Client.Server.Packages.LettersDir
{
    public class ReplyLetter : RequestObject
    {
        public string Answer { get; set; }
        public string Source { get; set; }
        public string Responder { get; set; }
        public int ResponderId { get; set; }
        public int LetterId { get; set; }
        [JsonConstructor]
        public ReplyLetter(string answer, string source, string responder, int letterId)
        {
            Answer = answer;
            Source = source;
            Responder = responder;
            LetterId = letterId;
        }
        public ReplyLetter(string answer, int responderId, int letterId)
        {
            Answer = answer;
            LetterId = letterId;
            ResponderId = responderId;
        }
    }
}
