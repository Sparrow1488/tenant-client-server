using JumboServer;
using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Multi_Server_Test.Server.Blocks.Auth
{
    public class GetNewsCollectionModel : Model
    {
        //private ServerModulEvents serverEvents = new ServerModulEvents();
        public GetNewsCollectionModel(string modelAction, bool forOnlyAdmin) : base(modelAction, forOnlyAdmin) { }

        public override byte[] CompleteAction(object reqObject)
        {
            List<News> responseNewsCollection = new List<News>();
            var newsCollectionToResponse = from news in MyServer.allNews 
                                           orderby news.DateTime descending
                                           select news;
            foreach (var news in newsCollectionToResponse)
            {
                responseNewsCollection.Add(news);
            }
            byte[] response;
            try
            {
                var jsonNewsCollection = JsonConvert.SerializeObject(responseNewsCollection);
                response = ServerMeta.Encoding.GetBytes(jsonNewsCollection);
                return response;
            }
            catch (Exception) 
            {
                return ServerMeta.Encoding.GetBytes("Неизвестная ошибка");
            }
        }
    }
}
