using Multi_Server_Test.Server.Models;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Server_Test.Server.Blocks.Auth
{
    public class GetNewsCollectionModel : Model
    {
        //private ServerModulEvents serverEvents = new ServerModulEvents();
        public GetNewsCollectionModel(string modelAction) : base(modelAction) { }

        public override async Task<byte[]> CompleteAction(object reqObject)
        {
            var newsCollectionToResponse =  MyServer.newsCollectionOutDB;
            byte[] response;
            try
            {
                var jsonNewsCollection = JsonConvert.SerializeObject(newsCollectionToResponse);
                response = Encoding.UTF8.GetBytes(jsonNewsCollection);
                //await new NewsView(response, stream).ExecuteModuleProcessing("");
                return response;
            }
            catch (Exception) 
            {
                return Encoding.UTF8.GetBytes("Неизвестная ошибка");
            }
        }
    }
}
