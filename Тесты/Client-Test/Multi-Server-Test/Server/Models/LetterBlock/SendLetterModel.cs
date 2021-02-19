using Multi_Server_Test.Server.Views;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Server_Test.Server.Blocks.LetterBlock
{
    public class SendLetterModel : Model
    {
        private ServerModulEvents serverEvents = new ServerModulEvents();
        public SendLetterModel(string modelAction) : base(modelAction) { }
        public override async void CompleteAction(object reqObject, NetworkStream stream)
        {
            try
            {
                var getJsonLetter = JsonConvert.SerializeObject(reqObject);
                var getLetter = JsonConvert.DeserializeObject<Letter>(getJsonLetter);
                serverEvents.BlockReport(this, "Письмо успешно получено", ConsoleColor.Green);
                Console.WriteLine(getLetter); //TODO: сделать сортер по типу новости (предложение, жалоба, вопрос)
                var data = Encoding.UTF8.GetBytes("Письмо получено");
                await new LetterView(data, stream).ExecuteModuleProcessing("");
            }
            catch (Exception) { }
        }
    }
}
