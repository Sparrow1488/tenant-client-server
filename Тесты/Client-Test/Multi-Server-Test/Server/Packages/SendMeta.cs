using Multi_Server_Test.Server.Models.AuthBlock;
using Newtonsoft.Json;

namespace Multi_Server_Test.Server.Packages
{
    public class SendMeta
    {
        [JsonConstructor]
        public SendMeta(string address, string action, string fromHostName, UserToken userToken)
        {
            Address = address;
            Action = action;
            FromHostName = fromHostName;
            UserToken = userToken;
        }
        public string Address { get; }
        public string Action { get; }
        public string FromHostName { get; }
        public UserToken UserToken { get; }
        public override string ToString()
        {
            return $"Получена мета:\n" +
                   $"To: {Address},\n" +
                   $"From: {FromHostName},\n" +
                   $"Action: {Action}";
        }
    }
}
