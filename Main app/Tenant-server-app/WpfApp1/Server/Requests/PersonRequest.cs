using System; 
using WpfApp1.Blocks;
using WpfApp1.Server.Packages;

namespace WpfApp1.Classes.Client.Requests
{
    public class PersonRequest : Package
    {
        public PersonRequest(RequestObject request, SendMeta meta) : base(request, meta)
        {

        }
        //public Person DataPerson { get; }
        //public SendMeta DataMeta { get; }
    }
}
