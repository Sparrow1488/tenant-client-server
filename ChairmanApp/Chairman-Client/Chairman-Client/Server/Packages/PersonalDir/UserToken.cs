using Newtonsoft.Json;
using System;

namespace WpfApp1.Server.Packages.PersonalDir
{
    public class UserToken : RequestObject
    {
        //public UserToken ActiveToken = null;
        public string SynchronizationPassword { get; }
        public string EncodedLogin { get; }
        [JsonConstructor]
        public UserToken(string synchronizationPassword, string encodedLogin)
        {
            SynchronizationPassword = synchronizationPassword;
            EncodedLogin = encodedLogin;
        }
        public static UserToken TryDeserializeToken(string jsonToken)
        {
            try
            {
                var token = JsonConvert.DeserializeObject<UserToken>(jsonToken);
                if (!string.IsNullOrWhiteSpace(token.EncodedLogin) && !string.IsNullOrWhiteSpace(token.SynchronizationPassword))
                    return token;
                else
                    return null;
            }
            catch(JsonException) { return null; }
        }
    }
}
