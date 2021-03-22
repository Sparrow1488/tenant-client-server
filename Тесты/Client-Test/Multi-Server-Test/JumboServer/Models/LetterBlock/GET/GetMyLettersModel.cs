using JumboServer.Meta;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using JumboServer.Functions;
using JumboServer.API;

namespace JumboServer.Models.LetterBlock.GET
{
    public class GetMyLettersModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public GetMyLettersModel(string modelAction, bool forOnlyAdmin) : base(modelAction, forOnlyAdmin) { }

        public override byte[] CompleteAction(object reqObject)
        {
            byte[] response = ServerMeta.Encoding.GetBytes("Неизвестная ошибка");
            try
            {
                var getPerson = JsonConvert.DeserializeObject<Person>(reqObject.ToString());
                //var personLetters = serverFunctions.GetPersonalLetterByUserId(getPerson.Id);
                var personLetters = serverFunctions.GetPersonalLetterByUserIdOutDB(getPerson.Id);
                if (personLetters != null)
                {
                    var replyJson = JsonConvert.SerializeObject(personLetters);
                    response = ServerMeta.Encoding.GetBytes(replyJson);
                }
                return response;
            }
            catch (Exception)
            {
                var exMessage = "Неизвестная ошибка";
                serverEvents.BlockReport(this, exMessage, ConsoleColor.Red);
                return ServerMeta.Encoding.GetBytes(exMessage);
            }
        }
    }
}
