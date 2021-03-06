﻿using Chairman_Client.Chairman.Packages;
using Chairman_Client.Server.Packages.LettersDir;
using Multi_Server_Test.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfApp1.Server.Packages.Letters;
using WpfApp1.Server.ServerMeta;

namespace Chairman_Client.Server.Chairman.Functions
{
    public class Functions
    {
        public static Functions Active;
        public Functions(JumboServer server)
        {
            if(server != null)
                Active = this;
        }
        public async Task<List<Letter>> GetLetters()
        {
            try
            {
                var requestPackage = new GetLettersPackage();
                var jsonListLetters = await JumboServer.ActiveServer.SendAndGetAsync(requestPackage);
                var listLetters = JsonConvert.DeserializeObject<List<Letter>>(jsonListLetters);
                return listLetters;
            }
            catch (JsonReaderException)
            {
                return null;
            }
        }
        /// <summary>
        /// Отправляет запись на сервер
        /// </summary>
        /// <param name="news">Новая запись</param>
        /// <returns>"1" - если запись успешно добавлена; "0" - если запись не прошла проверку валидации; "-1" - неизвестная ошибка сервера</returns>
        public async Task<string> AddNews(News news)
        {
            var pack = new AddNewsPackage(news);
            var response = await JumboServer.ActiveServer.SendAndGetAsync(pack);
            return response;
        }
        public async Task<string> SendReplyToLetter(ReplyLetter answer)
        {
            try
            {
                var package = new SendAnswerToLetterPackage(answer);
                var answerawait = await JumboServer.ActiveServer.SendAndGetAsync(package);
                return answerawait;
            }
            catch { }
            return "Неизвестная ошибка";
        }
    }
}
