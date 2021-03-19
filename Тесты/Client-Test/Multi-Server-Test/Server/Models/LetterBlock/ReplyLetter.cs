﻿using Newtonsoft.Json;

namespace Multi_Server_Test.Server.Models.LetterBlock
{
    public class ReplyLetter
    {
        public string Answer { get; }
        public string Source { get; }
        public string Responder { get; }
        public int LetterId { get; }
        public int ResponderId { get; }
        [JsonConstructor]
        public ReplyLetter(string answer, string source, string responder, int letterId, int responderId)
        {
            Answer = answer;
            Source = source;
            Responder = responder;
            LetterId = letterId;
            ResponderId = responderId;
        }
    }
}
