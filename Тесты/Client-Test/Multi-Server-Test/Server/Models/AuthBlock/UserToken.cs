using Newtonsoft.Json;

namespace Multi_Server_Test.Server.Models.AuthBlock
{
    public class UserToken
    {
        public string SynchronizationPassword { get; private set; }
        public string EncodedLogin { get; private set; }
        [JsonConstructor]
        public UserToken(string synchronizationPassword, string encodedLogin)
        {
            SynchronizationPassword = synchronizationPassword;
            EncodedLogin = encodedLogin;
        }
        
        private UserToken TryDeserializeToken(string jsonToken)
        {
            try
            {
                var token = JsonConvert.DeserializeObject<UserToken>(jsonToken);
                if (!string.IsNullOrWhiteSpace(token.EncodedLogin) && !string.IsNullOrWhiteSpace(token.SynchronizationPassword))
                    return token;
                else
                    return null;
            }
            catch (JsonException) { return null; }
        }
    }
}
