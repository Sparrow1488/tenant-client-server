using ExchangeSystem.v2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantClient.Pages;

namespace TenantClient.AdditionalStructs
{
    public class ReadLetterStruct
    {
        public ReadLetterStruct(Letter letter)
        {
            ReadLetter = letter;
            //Page = new ReadLetter(letter);
            Sendler = ReadLetter.sendler;
        }
        public Letter ReadLetter { get; set; }
        public ReadLetter Page { get; set; }
        public string Sendler { get; set; }
    }
}
