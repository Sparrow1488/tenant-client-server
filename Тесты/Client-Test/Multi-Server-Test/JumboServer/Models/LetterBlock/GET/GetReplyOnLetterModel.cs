using JumboServer.Meta;
using Newtonsoft.Json;
using System;
using System.Text;
using JumboServer.Functions;
using JumboServer.API;

namespace JumboServer.Models.LetterBlock.GET
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
