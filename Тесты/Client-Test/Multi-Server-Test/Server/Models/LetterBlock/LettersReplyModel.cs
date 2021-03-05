using Multi_Server_Test.Server.Blocks.LetterBlock;
using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Models.LetterBlock
{
    public class LettersReplyModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public LettersReplyModel(string modelAction) : base(modelAction) { }

        public override byte[] CompleteAction(object reqObject)
        {
            try
            {
                byte[] response;
                string replyJson = JsonConvert.SerializeObject(reqObject);
                var replyObj = JsonConvert.DeserializeObject<ReplyLetter>(replyJson);
                serverEvents.BlockReport(this, "Запрос на добавление ответа письму", ConsoleColor.Yellow);
                int successCompl = serverFunctions.ReplyToTheLetter(replyObj);
                if(successCompl > 0)
                {
                    response = Encoding.UTF8.GetBytes("Ответ успешно добавлен");
                    serverEvents.BlockReport(this, "Ответ успешно добавлен", ConsoleColor.Green);
                }
                else
                {
                    response = Encoding.UTF8.GetBytes("Ошибка записи ответа");
                    serverEvents.BlockReport(this, "Ошибка записи ответа", ConsoleColor.Red);
                }
                return response;
            }
            catch (Exception)
            {
                return Encoding.UTF8.GetBytes("Неизвестная ошибка");
            }
        }
    }
}
