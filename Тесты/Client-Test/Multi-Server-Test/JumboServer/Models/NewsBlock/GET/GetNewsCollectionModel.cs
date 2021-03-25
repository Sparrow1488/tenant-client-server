using JumboServer;
using JumboServer.Meta;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using JumboServer.API;

namespace JumboServer.Models.NewsBlock.GET
{
    public class GetNewsCollectionModel : Model
    {
        //private ServerModulEvents serverEvents = new ServerModulEvents();
        public GetNewsCollectionModel(string modelAction, bool forOnlyAdmin) : base(modelAction, forOnlyAdmin) { }

        public override byte[] CompleteAction(object reqObject)
        {
            List<News> responseNewsCollection = new List<News>();
            responseNewsCollection = MyServer.allNews.OrderByDescending(news => news.DateTime).ToList();
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
