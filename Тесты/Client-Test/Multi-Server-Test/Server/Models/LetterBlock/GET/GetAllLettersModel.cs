using Multi_Server_Test.Server.Blocks.LetterBlock;
using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Server;
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
        public GetAllLettersModel(string modelAction, bool forOnlyAdmin) : base(modelAction, forOnlyAdmin) { }

        public override byte[] CompleteAction(object reqObject)
        {
            try
            {
                byte[] response;
                serverEvents.BlockReport(this, "Запрос на получение новостей", ConsoleColor.Green);
                List<Letter> lettersOutDB = serverFunctions.GetAllLettersOutDB();

                if (lettersOutDB == null)
                {
                    response = ServerMeta.Encoding.GetBytes("Список писем пока пуст");
                }
                else
                {
                    string responseLetters = JsonConvert.SerializeObject(lettersOutDB);
                    response = ServerMeta.Encoding.GetBytes(responseLetters);
                }
                return response;
            }
            catch (Exception) 
            {
                return ServerMeta.Encoding.GetBytes("Неизвестная ошибка");
            }
        }
    }
}
