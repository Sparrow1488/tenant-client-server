using System;

namespace WpfApp1.Server.ServerExceptions
{
    public class UserNotExist : JumboServerException
    {
        public UserNotExist(string message) : base(message)
        {
        }
    }
}
