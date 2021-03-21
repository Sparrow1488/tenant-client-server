using Multi_Server_Test.Server.Blocks.LetterBlock;
using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Server;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Multi_Server_Test.Server.Models.LetterBlock
{
    public class GetReplyOnLetterModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public GetReplyOnLetterModel(string modelAction, bool forOnlyAdmin) : base(modelAction, forOnlyAdmin) { }

        public override byte[] CompleteAction(object reqObject)
        {
            byte[] response = ServerMeta.Encoding.GetBytes("Неизвестная ошибка");
            try
            {
                var getLetter = JsonConvert.DeserializeObject<Letter>(reqObject.ToString());
                var replyes = serverFunctions.GetReplyByLetterId(getLetter.Id);
                if (replyes != null)
                {
                    serverEvents.BlockReport(this, "Ответы на письма получены", ConsoleColor.Green);
                    var replyJson = JsonConvert.SerializeObject(replyes);
                    response = ServerMeta.Encoding.GetBytes(replyJson);
                }
                return response;
            }
            catch (Exception)
            {
                var exMessage = "Неизвестная ошибка";
                serverEvents.BlockReport(this, exMessage, ConsoleColor.Red);
                return ServerMeta.Encoding.GetBytes(exMessage);
            }
        }
    }
}
