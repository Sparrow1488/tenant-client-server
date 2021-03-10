using Multi_Server_Test.Server.Blocks.LetterBlock;
using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Models.LetterBlock
{
    public class GetAllLettersModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public GetAllLettersModel(string modelAction) : base(modelAction) { }

        public override byte[] CompleteAction(object reqObject)
        {
            try
            {
                byte[] response;
                serverEvents.BlockReport(this, "Запрос на получение новостей", ConsoleColor.Green);
                List<Letter> lettersOutDB = serverFunctions.GetAllLettersOutDB();

                if (lettersOutDB == null)
                {
                    response = Encoding.UTF8.GetBytes("Список писем пока пуст");
                }
                else
                {
                    string responseLetters = JsonConvert.SerializeObject(lettersOutDB);
                    response = Encoding.UTF8.GetBytes(responseLetters);
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
