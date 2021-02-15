using System;

namespace WpfApp1.Server.Packages.Letters
{
    public class ComplaintLetter : Letter
    {
        public ComplaintLetter(string title, string description, Person sender) : base(title, description, sender)
        {
            LetterType = "complaint";
        }
    }
}
