using System;

namespace WpfApp1.Server.ServerExceptions
{
    public class ConnectionException : JumboServerException
    {
        public ConnectionException(string message) : base(message) { }
    }
}
