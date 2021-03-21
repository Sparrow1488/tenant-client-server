using JumboServer;
using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Server;
using Newtonsoft.Json;
using System;

namespace Multi_Server_Test.Server.Models.NewsBlock
{
    public class AddNewsModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public AddNewsModel(string modelAction, bool forOnlyAdmin) : base(modelAction, forOnlyAdmin) { }

        public override byte[] CompleteAction(object reqObject)
        {
            string responseMessage = "Неизвестная ошибка";
            var jsonNews = JsonConvert.SerializeObject(reqObject);
            var newsForInsert = JsonConvert.DeserializeObject<News>(jsonNews);

            bool canInsertInDB = CheckNewsValidation(newsForInsert);

            if (canInsertInDB)
            {
                var success = serverFunctions.InsertNewsInDB(newsForInsert); //TODO: сделать таймер для автоматической синхронизации коллекций
                if (success > 0)
                {
                    MyServer.allNews.Add(newsForInsert);
                    MyServer.noSynchNews.Add(newsForInsert);

                    serverEvents.BlockReport(this, "Успешно выполнено запросов: " + success, ConsoleColor.Green);
                    responseMessage = "Новость успешно опубликована";
                }
                else
                {
                    serverEvents.BlockReport(this, "Выполнено запросов: " + success, ConsoleColor.Red);
                    responseMessage = "Ошибка публикации";
                }
            }
            else
            {
                serverEvents.BlockReport(this, "Ошибка публикации: не пройдено валидацию", ConsoleColor.Yellow);
                responseMessage = "Ошибка валидации";
            }

            return ServerMeta.Encoding.GetBytes(responseMessage);
        }
        private bool CheckNewsValidation(News news)
        {
            if(string.IsNullOrWhiteSpace(news.Title) || string.IsNullOrWhiteSpace(news.Description))
            {
                return false;
            }
            return true;
        }
        //private void PreapareResponse(string response, string serverMessage, ConsoleColor color)
        //{
        //    serverEvents.BlockReport(this, "Выполнено запросов: " + success, ConsoleColor.Red);
        //    responseMessage = "Ошибка публикации";
        //}
    }
}
