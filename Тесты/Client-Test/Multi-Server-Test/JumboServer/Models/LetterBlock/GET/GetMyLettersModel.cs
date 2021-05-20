using JumboServer.Meta;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using JumboServer.Functions;
using JumboServer.API;
using System.Linq;

namespace JumboServer.Models.LetterBlock.GET
{
    public class GetMyLettersModel
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();

        public byte[] CompleteAction(object reqObject)
        {
            var sortUserLetters = new List<Letter>();
            byte[] response = ServerMeta.Encoding.GetBytes("Неизвестная ошибка");
            try
            {
                var getPerson = JsonConvert.DeserializeObject<Person>(reqObject.ToString());
                var lettersOutDB = serverFunctions.GetPersonalLetterByUserIdOutDB(getPerson.Id);
                sortUserLetters = lettersOutDB.OrderByDescending(letter => letter?.DateCreate).ToList();
                
                var replyJson = JsonConvert.SerializeObject(sortUserLetters);
                response = ServerMeta.Encoding.GetBytes(replyJson);
                return response;
            }
            catch (Exception)
            {
                var exMessage = "Неизвестная ошибка";
                serverEvents.BlockReport("GetMyNews", exMessage, ConsoleColor.Red);
                return ServerMeta.Encoding.GetBytes(exMessage);
            }
        }
    }
}
