using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Models.LetterBlock
{
    public class ReplyLetter
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
