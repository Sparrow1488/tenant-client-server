using System;

namespace WpfApp1.Server.Packages.Letters
{
    public class ComplaintLetter : Letter
    {
        public ComplaintLetter(string title, string description, int senderId, string[] sourcesTokens) : base(title, description, senderId, sourcesTokens)
        {
            LetterType = "complaint";
        }
    }
}
