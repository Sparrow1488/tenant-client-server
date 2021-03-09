using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Linq;
using Multi_Server_Test.Server.Packages;
using System.Collections.Generic;

namespace Multi_Server_Test.Server.Blocks.Auth
{
    public class GetNewsCollectionModel : Model
    {
        //private ServerModulEvents serverEvents = new ServerModulEvents();
        public GetNewsCollectionModel(string modelAction) : base(modelAction) { }

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
