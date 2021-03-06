﻿using JumboServer.API;
using JumboServer.Functions;
using JumboServer.Meta;
using Newtonsoft.Json;
using System;

namespace JumboServer.Models.NewsBlock.ADD
{
    public class AddNewsModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public AddNewsModel(string modelAction, bool forOnlyAdmin) : base(modelAction, forOnlyAdmin) { }

        public override byte[] CompleteAction(object reqObject)
        {
            string responseMessage = "-1";
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
                    responseMessage = "1";
                }
                else
                {
                    serverEvents.BlockReport(this, "Выполнено запросов: " + success, ConsoleColor.Red);
                    responseMessage = "-1";
                }
            }
            else
            {
                serverEvents.BlockReport(this, "Ошибка публикации: не пройдено валидацию\nУказания: возможно, текст слишком мал для публикации", ConsoleColor.Yellow);
                responseMessage = "0";
            }

            return ServerMeta.Encoding.GetBytes(responseMessage);
        }
        private bool CheckNewsValidation(News news)
        {
            if(news.Title.Length > 5 && news.Description.Length > 10 && news.Title != "Заголовок" && news.Description != "Описание")
            {
                return true;
            }
            return false;
        }
    }
}
