﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Server.Packages.Letters
{
    public class OfferLetter : Letter
    {
        //public OfferLetter(string title, string description, int senderId, string[] sourcesTokens) : base(title, description, senderId, sourcesTokens)
        //{
        //    LetterType = "offer";
        //}
        public OfferLetter(string title, string description, int senderId, int letterType) : base(title, description, senderId, letterType)
        {
        }
    }
}
