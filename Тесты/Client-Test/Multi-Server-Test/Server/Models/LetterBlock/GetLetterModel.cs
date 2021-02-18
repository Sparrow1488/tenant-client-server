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
    public class GetLetterModel : Model
    {
        public GetLetterModel(string modelAction) : base(modelAction)
        {
        }

        //private ServerModelEvents serverEvents = new ServerModelEvents();
        //public GetLetterModel(string blockAction) : base(blockAction) { }

        //public override async void CompleteAction(string clientJson, NetworkStream stream)
        //{
        //    try
        //    { 
        //        var getLetter = JsonConvert.DeserializeObject<Letter>(clientJson);
        //        serverEvents.BlockReport(this, "Письмо успешно получено", ConsoleColor.Green);
        //        Console.WriteLine(getLetter); //TODO: сделать сортер по типу новости (предложение, жалоба, вопрос)
        //        var data = Encoding.UTF8.GetBytes("Письмо получено");
        //        await stream.WriteAsync(data, 0, data.Length);
        //    }
        //    catch (Exception) { }
        //} 
        public override void CompleteAction(object reqObject, NetworkStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
