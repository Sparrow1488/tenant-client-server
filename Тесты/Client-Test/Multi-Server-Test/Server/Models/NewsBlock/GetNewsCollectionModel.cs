using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Multi_Server_Test.Server.Blocks.Auth
{
    public class GetNewsCollectionModel : Model
    {
        //private ServerModulEvents serverEvents = new ServerModulEvents();
        public GetNewsCollectionModel(string modelAction) : base(modelAction) { }

        public override byte[] CompleteAction(object reqObject)
        {
            var newsCollectionToResponse = MyServer.newsCollectionOutDB;
            byte[] response;
            try
            {
                var jsonNewsCollection = JsonConvert.SerializeObject(newsCollectionToResponse);
                response = Encoding.UTF8.GetBytes(jsonNewsCollection);
                return response;
            }
            catch (Exception) 
            {
                return Encoding.UTF8.GetBytes("Неизвестная ошибка");
            }
        }
    }
}
