using JumboServer;
using JumboServer.Meta;
using Newtonsoft.Json;
using System;
using JumboServer.Functions;
using JumboServer.API;

namespace JumboServer.Models.NewsBlock.ADD
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
