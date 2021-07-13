using ExchangeSystem.v2.Entities;

namespace TenantClient.AdditionalStructs
{
    public class ReadLetterStruct
    {
        public ReadLetterStruct(Letter letter)
        {
            ReadLetter = letter;
            Sendler = ReadLetter.sendler;
        }
        public Letter ReadLetter { get; set; }
        public string Sendler { get; set; }
    }
}
