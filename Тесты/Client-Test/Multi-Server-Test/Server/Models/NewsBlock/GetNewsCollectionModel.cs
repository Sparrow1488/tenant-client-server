using Multi_Server_Test.Server.Models;
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
        private ServerModulEvents serverEvents = new ServerModulEvents();
        public GetNewsCollectionModel(string modelAction) : base(modelAction) { }

        public override async void CompleteAction(object reqObject, NetworkStream stream)
        {
            var newsCollectionToResponse =  MyServer.newsCollectionOutDB;
            try
            {
                var jsonNewsCollection = JsonConvert.SerializeObject(newsCollectionToResponse);
                var response = Encoding.UTF8.GetBytes(jsonNewsCollection);
                await new NewsView(response, stream).ExecuteModuleProcessing("");
            }
            catch (Exception) { }
        }
    }
}
