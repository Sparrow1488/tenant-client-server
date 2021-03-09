using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Multi_Server_Test.Server.Models.NewsBlock
{
    public class AddNewsModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public AddNewsModel(string modelAction) : base(modelAction) { }

        public override byte[] CompleteAction(object reqObject)
        {
            string responseMessage = "";
            var jsonNews = JsonConvert.SerializeObject(reqObject);
            var newsForInsert = JsonConvert.DeserializeObject<News>(jsonNews);
            MyServer.allNews.Add(newsForInsert);
            MyServer.noSynchNews.Add(newsForInsert);
            var success = serverFunctions.InsertNewsInDB(newsForInsert); //TODO: сделать таймер для автоматической синхронизации коллекций

            if (success > 0)
            {
                serverEvents.BlockReport(this, "Успешно выполнено запросов: " + success, ConsoleColor.Green);
                responseMessage = "Новость успешно опубликована";
            }
            else
            {
                serverEvents.BlockReport(this, "Выполнено запросов: " + success, ConsoleColor.Red);
                responseMessage = "Ошибка публикации";
            }

            return Encoding.UTF8.GetBytes(responseMessage);
        }
    }
}
