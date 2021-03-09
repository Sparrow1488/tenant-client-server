﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Server.Packages.Letters
{
    public class QuestionLetter : Letter
    {
        public QuestionLetter(string title, string description, int sender) : base(title, description, sender)
        {
            LetterType = "question";
        }
    }
}
