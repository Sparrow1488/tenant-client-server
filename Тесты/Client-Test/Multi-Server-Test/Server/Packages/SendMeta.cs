using Newtonsoft.Json;

namespace Multi_Server_Test.Server.Packages
{
    public class SendMeta
    {
        [JsonConstructor]
        public SendMeta(string address, string action, string fromHostName)
        {
            Address = address;
            Action = action;
            FromHostName = fromHostName;
        }
        public string Address { get; }
        public string Action { get; }
        public string FromHostName { get; }
        public override string ToString()
        {
            return $"Получена мета:\n" +
                   $"To: {Address},\n" +
                   $"From: {FromHostName},\n" +
                   $"Action: {Action}";
        }
    }
}
