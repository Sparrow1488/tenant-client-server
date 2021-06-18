using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantClient.Exceptions
{
    public class TokenException : ClientException
    {
        public TokenException(string message) : base(message)
        {
        }
    }
}
