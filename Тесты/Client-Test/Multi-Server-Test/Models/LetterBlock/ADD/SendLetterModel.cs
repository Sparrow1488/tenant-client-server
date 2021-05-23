using JumboServer.API;
using JumboServer.Functions;
using JumboServer.Meta;
using Newtonsoft.Json;
using System;

namespace JumboServer.Models.LetterBlock.ADD
{
    public class SendLetterModel
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();

        public byte[] CompleteAction(object reqObject)
        {
            byte[] response = ServerMeta.Encoding.GetBytes("-1");
            try
            {
                var getLetter = JsonConvert.DeserializeObject<Letter>(reqObject.ToString());
                Console.WriteLine(getLetter);

                bool canInsertInDB = CheckValidation(getLetter);

                if (canInsertInDB)
                {
                    var existTokens = serverFunctions.ReturnExistTokens(getLetter.SourcesTokens);
                    getLetter.SourcesTokens = existTokens;

                    int successInsert = serverFunctions.AddLetterInDB(getLetter);
                    MyServer.AddLetterInLocalStorage(getLetter);
                    response = ServerMeta.Encoding.GetBytes("1");
                }
                else
                {
                    response = ServerMeta.Encoding.GetBytes("0");
                }
                
                return response;
            }
            catch (Exception)
            {
                serverEvents.BlockReport("SendLetters", "Неизвестная ошибка", ConsoleColor.Red);
                return ServerMeta.Encoding.GetBytes("-1");
            }
        }
        private bool CheckValidation(Letter letter)
        {
            if (string.IsNullOrEmpty(letter.Title) || string.IsNullOrEmpty(letter.Description))
            {
                return false;
            }
            return true;
        }
        
    }
}
