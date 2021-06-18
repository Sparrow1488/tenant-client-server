using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantClient.Exceptions
{
    public class WindowException : ClientException
    {
        public WindowException(string message) : base(message) { }
    }
}
