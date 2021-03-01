using System;

namespace WpfApp1.Server.ServerExceptions
{
    public class GetResponseException : JumboServerException
    {
        public GetResponseException(string message) : base(message)
        {
        }
    }
}
