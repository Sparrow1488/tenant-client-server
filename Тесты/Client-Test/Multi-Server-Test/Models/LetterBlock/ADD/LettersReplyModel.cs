using JumboServer.API;
using JumboServer.Functions;
using JumboServer.Meta;
using Newtonsoft.Json;
using System;

namespace JumboServer.Models.LetterBlock.ADD
{
    public class LettersReplyModel
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();

        public byte[] CompleteAction(object reqObject)
        {
            try
            {
                byte[] response;
                var replyObj = JsonConvert.DeserializeObject<ReplyLetter>(reqObject.ToString());
                int successCompl = serverFunctions.ReplyToTheLetter(replyObj);
                if(successCompl > 0)
                    response = ServerMeta.Encoding.GetBytes("1");
                else
                    response = ServerMeta.Encoding.GetBytes("-1");
                return response;
            }
            catch (Exception)
            {
                serverEvents.BlockReport("ReplyLetters", "Неизвестная ошибка", ConsoleColor.Red);
                return ServerMeta.Encoding.GetBytes("-1");
            }
        }
    }
}
