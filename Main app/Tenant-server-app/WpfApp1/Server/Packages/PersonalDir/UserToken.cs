﻿using Newtonsoft.Json;

namespace WpfApp1.Server.Packages.PersonalDir
{
    public class UserToken
    {
        public UserToken ActiveToken = null;
        private string SynchronizationPassword { get; }
        private string EncodedLogin { get; }
        public UserToken(string jsonToken)
        {
            var token = TryDeserializeToken(jsonToken);
            ActiveToken = token;
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
            catch(JsonException) { return null; }
        }
    }
}
