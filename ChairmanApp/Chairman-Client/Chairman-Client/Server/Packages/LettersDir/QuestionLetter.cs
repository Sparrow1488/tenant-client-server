using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChairmanClient.Server.Packages.Letters
{
    public class QuestionLetter : Letter
    {
        public QuestionLetter(string title, string description, Person sender) : base(title, description, sender)
        {
            LetterType = "question";
        }
    }
}
