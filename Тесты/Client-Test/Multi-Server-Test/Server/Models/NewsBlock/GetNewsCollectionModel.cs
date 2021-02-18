using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

namespace Multi_Server_Test.Server.Blocks.Auth
{
    public class GetNewsCollectionModel : Model
    {
        public GetNewsCollectionModel(string modelAction) : base(modelAction)
        {
        }

        //private ServerModelEvents serverEvents = new ServerModelEvents();
        //public GetNewsCollectionModel(string blockAction) : base(blockAction) { }

        //public override async void CompleteAction(string clientJson, NetworkStream stream)
        //{
        //    var newsCollectionToResponse = ResponseStream.newsCollectionOutDB;

        //    try
        //    {
        //        var jsonNewsCollection = JsonConvert.SerializeObject(newsCollectionToResponse);
        //        var response = Encoding.UTF8.GetBytes(jsonNewsCollection);
        //        await stream.WriteAsync(response, 0, response.Length);

        //        serverEvents.BlockReport(this, "Коллекция новостей отправлена", ConsoleColor.Green);
        //    }
        //    catch (Exception) { }
        //}
        public override void CompleteAction(object reqObject, NetworkStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
