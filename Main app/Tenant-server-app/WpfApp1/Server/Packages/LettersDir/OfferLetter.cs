using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Server.Packages.Letters
{
    public class OfferLetter : Letter
    {
        public OfferLetter(string title, string description, string sender) : base(title, description, sender)
        {
            LetterType = "offer";
        }
    }
}
