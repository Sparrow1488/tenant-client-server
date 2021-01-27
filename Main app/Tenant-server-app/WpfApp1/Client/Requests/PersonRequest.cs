using System; 
using WpfApp1.Blocks;

namespace WpfApp1.Classes.Client.Requests
{
    public class PersonRequest : IRequest
    {
        public PersonRequest(IForRequest request, Meta meta) : base(request, meta)
        {

        }

        public Person DataPerson { get; set; }
        public Meta DataMeta { get; set; }
    }
}
