using Multi_Server_Test.Server.Blocks.LetterBlock;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Models.LetterBlock
{
    public class GetLetterModel : Model
    {
        private ServerModulEvents serverEvents = new ServerModulEvents();
        public GetLetterModel(string modelAction) : base(modelAction) { }

        public override byte[] CompleteAction(object reqObject)
        {
            try
            {
                serverEvents.BlockReport(this, "Запрос на получение новостей", ConsoleColor.Green);
                List<Letter> lettersOutDB = GetLettersCollection();
                byte[] response;

                if (lettersOutDB == null)
                    response = Encoding.UTF8.GetBytes("Список писем пока пуст");
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
        private List<Letter> GetLettersCollection()
        {
            return MyServer.allLetters; // TODO: сделать дичь с доставанием новостей с сервера
        }
    }
}
