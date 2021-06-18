using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantClient.Exceptions
{
    public class ClientException : Exception
    {
        public ClientException() { }
        public ClientException(string message) : base(message) { }
    }
}
