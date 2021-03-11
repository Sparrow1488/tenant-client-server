using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Text;

namespace Multi_Server_Test.Server.Blocks.LetterBlock
{
    public class SendLetterModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public SendLetterModel(string modelAction) : base(modelAction) { }
        public override byte[] CompleteAction(object reqObject)
        {
            byte[] response = Encoding.UTF8.GetBytes("-1");
            try
            {
                var getLetter = JsonConvert.DeserializeObject<Letter>(reqObject.ToString());
                serverEvents.BlockReport(this, "Письмо успешно получено", ConsoleColor.Yellow);
                Console.WriteLine(getLetter);

                MyServer.allLetters.Add(getLetter);
                MyServer.noSynchLetters.Add(getLetter);

                var canInsertInDB = CheckValidation(getLetter);

                if (canInsertInDB)
                {
                    int successInsert = serverFunctions.AddLetterInDB(getLetter);
                    serverEvents.BlockReport(this, "Успешно добавлено писем: " + successInsert, ConsoleColor.Green);
                    response = Encoding.UTF8.GetBytes("1");
                }
                else
                {
                    serverEvents.BlockReport(this, "Ошибка валидации полученного письма: не добавлено в БД", ConsoleColor.Yellow);
                    response = Encoding.UTF8.GetBytes("-1");
                }
                
                return response;
            }
            catch (Exception)
            {
                serverEvents.BlockReport(this, "Неизвестная ошибка", ConsoleColor.Red);
                return Encoding.UTF8.GetBytes("-1");
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
