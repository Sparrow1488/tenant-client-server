using JumboServer.Meta;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using JumboServer.Functions;
using JumboServer.API;

namespace JumboServer.Models.LetterBlock.GET
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
