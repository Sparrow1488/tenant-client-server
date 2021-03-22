using JumboServer.API;
using Newtonsoft.Json;

namespace JumboServer.Packages
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
