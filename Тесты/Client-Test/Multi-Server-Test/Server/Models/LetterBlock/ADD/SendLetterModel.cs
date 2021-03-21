using JumboServer;
using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Server;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Multi_Server_Test.Server.Blocks.LetterBlock
{
    public class SendLetterModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public SendLetterModel(string modelAction, bool forOnlyAdmin) : base(modelAction, forOnlyAdmin) { }
        public override byte[] CompleteAction(object reqObject)
        {
            byte[] response = ServerMeta.Encoding.GetBytes("-1");
            try
            {
                var getLetter = JsonConvert.DeserializeObject<Letter>(reqObject.ToString());
                serverEvents.BlockReport(this, "Письмо успешно получено", ConsoleColor.Yellow);
                Console.WriteLine(getLetter);

                bool canInsertInDB = CheckValidation(getLetter);

                if (canInsertInDB)
                {
                    var existTokens = serverFunctions.ReturnExistTokens(getLetter.SourcesTokens);
                    getLetter.SourcesTokens = existTokens;

                    int successInsert = serverFunctions.AddLetterInDB(getLetter);
                    serverEvents.BlockReport(this, "Успешно добавлено писем: " + successInsert, ConsoleColor.Green);
                    MyServer.AddLetterInLocalStorage(getLetter);
                    response = ServerMeta.Encoding.GetBytes("1");
                }
                else
                {
                    serverEvents.BlockReport(this, "Ошибка валидации полученного письма: не добавлено в БД", ConsoleColor.Yellow);
                    response = ServerMeta.Encoding.GetBytes("0");
                }
                
                return response;
            }
            catch (Exception)
            {
                serverEvents.BlockReport(this, "Неизвестная ошибка", ConsoleColor.Red);
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
