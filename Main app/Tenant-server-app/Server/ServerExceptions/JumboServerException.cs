using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Server.ServerExceptions
{
    public abstract class JumboServerException : Exception
    {
        public override string Message { get; }
        public JumboServerException(string message)
        {
            Message = message;
        }
    }
}
