using JumboServer.Meta;
using Newtonsoft.Json;
using System;
using System.Text;
using JumboServer.Functions;
using JumboServer.API;

namespace JumboServer.Models.LetterBlock.GET
{
    public class GetReplyOnLetterModel
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();

        public byte[] CompleteAction(object reqObject)
        {
            byte[] response = ServerMeta.Encoding.GetBytes("Неизвестная ошибка");
            try
            {
                var getLetter = JsonConvert.DeserializeObject<Letter>(reqObject.ToString());
                var replyes = serverFunctions.GetReplyByLetterId(getLetter.Id);
                if (replyes != null)
                {
                    var replyJson = JsonConvert.SerializeObject(replyes);
                    response = ServerMeta.Encoding.GetBytes(replyJson);
                }
                return response;
            }
            catch (Exception)
            {
                var exMessage = "Неизвестная ошибка";
                return ServerMeta.Encoding.GetBytes(exMessage);
            }
        }
    }
}
