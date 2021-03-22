using JumboServer.API;
using JumboServer.Functions;
using JumboServer.Meta;
using Newtonsoft.Json;
using System;

namespace JumboServer.Models.LetterBlock.ADD
{
    public class LettersReplyModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public LettersReplyModel(string modelAction, bool forOnlyAdmin) : base(modelAction, forOnlyAdmin) { }

        public override byte[] CompleteAction(object reqObject)
        {
            try
            {
                byte[] response;
                var replyObj = JsonConvert.DeserializeObject<ReplyLetter>(reqObject.ToString());
                serverEvents.BlockReport(this, "Запрос на добавление ответа на письмо", ConsoleColor.Yellow);
                int successCompl = serverFunctions.ReplyToTheLetter(replyObj);
                if(successCompl > 0)
                {
                    serverEvents.BlockReport(this, "Ответ успешно добавлен", ConsoleColor.Green);
                    response = ServerMeta.Encoding.GetBytes("1");
                }
                else
                {
                    serverEvents.BlockReport(this, "Ошибка записи ответа", ConsoleColor.Red);
                    response = ServerMeta.Encoding.GetBytes("-1");
                }
                return response;
            }
            catch (Exception)
            {
                serverEvents.BlockReport(this, "Неизвестная ошибка", ConsoleColor.Red);
                return ServerMeta.Encoding.GetBytes("-1");
            }
        }
    }
}
