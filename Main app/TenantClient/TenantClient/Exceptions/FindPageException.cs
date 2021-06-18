using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantClient.Exceptions
{
    public class FindPageException : ClientException
    {
        public FindPageException(string message) : base(message) { }
    }
}
