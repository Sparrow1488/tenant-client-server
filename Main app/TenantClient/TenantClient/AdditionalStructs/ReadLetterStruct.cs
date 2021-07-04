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
        public ReadLetterStruct(Letter letter, ReadLetter page)
        {
            ReadLetter = letter;
            Page = page;
        }
        public Letter ReadLetter { get; set; }
        public ReadLetter Page { get; set; }
    }
}
