using System;

namespace WpfApp1.Server.ServerExceptions
{
    public class SendRequestException : JumboServerException
    {
        public SendRequestException(string message) : base(message)
        {
        }
    }
}
