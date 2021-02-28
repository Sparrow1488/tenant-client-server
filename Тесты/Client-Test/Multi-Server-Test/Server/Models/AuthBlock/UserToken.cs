using Multi_Server_Test.Blocks;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Multi_Server_Test.Server.Models.AuthBlock
{
    public class UserToken
    {
        public string SynchronizationPassword { get; private set; }
        public string EncodedLogin { get; private set; }
        public UserToken(string synchPass, string encodLogin)
        {
            SynchronizationPassword = synchPass;
            EncodedLogin = encodLogin;
        }
        public static UserToken GenerateToken()
        {
            var rnd = new Random();
            var pas = $"{rnd.Next(5000,100000)}-{rnd.Next(0, 1000)}-{rnd.Next(200, 6000)}";
            var log = $"randomBulbins";
            return new UserToken(pas, log);
        }
        public static UserToken GenerateToken(Person person)
        {
            var rnd = new Random();
            var pas = $"{rnd.Next(5000, 100000)}-{rnd.Next(0, 1000)}-{rnd.Next(200, 6000)}";
            StringBuilder log = new StringBuilder("randomBulbins");
            for (int i = 0; i < person.Login.Length; i++)
            {
                log.Append(person.Login[i] + rnd.Next(100));
            }
            return new UserToken(pas, log.ToString());
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
