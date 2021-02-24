using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Multi_Server_Test.Server.Blocks.LetterBlock
{
    public class SendLetterModel : Model
    {
        private ServerModulEvents serverEvents = new ServerModulEvents();
        public SendLetterModel(string modelAction) : base(modelAction) { }
        public override byte[] CompleteAction(object reqObject)
        {
            byte[] response;
            try
            {
                var getJsonLetter = JsonConvert.SerializeObject(reqObject);
                var getLetter = JsonConvert.DeserializeObject<Letter>(getJsonLetter);
                serverEvents.BlockReport(this, "Письмо успешно получено", ConsoleColor.Green);
                Console.WriteLine(getLetter); //TODO: сделать сортер по типу новости (предложение, жалоба, вопрос)
                response = Encoding.UTF8.GetBytes("Письмо получено");
                return response;
            }
            catch (Exception) 
            {
                return Encoding.UTF8.GetBytes("Неизвестная ошибка");
            }
        }
    }
}
