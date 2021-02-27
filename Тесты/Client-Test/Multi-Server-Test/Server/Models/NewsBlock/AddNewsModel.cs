using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Text;

namespace Multi_Server_Test.Server.Models.NewsBlock
{
    public class AddNewsModel : Model
    {
        private ServerModulEvents serverEvents = new ServerModulEvents();
        public AddNewsModel(string modelAction) : base(modelAction) { }

        public override byte[] CompleteAction(object reqObject)
        {
            var jsonNews = JsonConvert.SerializeObject(reqObject);
            var newsForInsert = JsonConvert.DeserializeObject<News>(jsonNews);
            var success = InsertNewsInDB(newsForInsert);
            string responseMessage;
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
        private int InsertNewsInDB(News news)
        {
            try
            {
                string sCommand = "INSERT INTO [News] (Title, Description, DateCreate, Sender, Type) VALUES (@title, @desc, @date, @sender, @type)";
                using (var command = new SqlCommand(sCommand, MyServer.Meta.sqlConnection))
                {
                    var validNews = CheckValidation(news);
                    command.Parameters.AddWithValue("title", validNews.Title);
                    command.Parameters.AddWithValue("desc", validNews.Description);
                    command.Parameters.AddWithValue("date", validNews.DateTime);
                    command.Parameters.AddWithValue("sender", validNews.Sender);
                    command.Parameters.AddWithValue("type", validNews.Type);
                    var successInsert = command.ExecuteNonQuery();
                    return successInsert;
                }
            }
            catch(Exception) { return -1; }
        }
        private News CheckValidation(News checkNews)
        {
            string validTitle = "", validDesc = "", validSource = "", validType = "";
            DateTime validDate = DateTime.MinValue;
            string validSender = "noname";
            if (!string.IsNullOrWhiteSpace(checkNews.Title))
                validTitle = checkNews.Title;
            if (!string.IsNullOrWhiteSpace(checkNews.Description))
                validDesc = checkNews.Description;
            if (!string.IsNullOrWhiteSpace(checkNews.Source))
                validSource = checkNews.Source;
            if (!string.IsNullOrWhiteSpace(checkNews.Type))
                validType = checkNews.Type;
            if (checkNews.DateTime != null)
                validDate = checkNews.DateTime;
            if (!string.IsNullOrWhiteSpace(checkNews.Sender))
                validSender = checkNews.Sender;
            var validNews = new News(validTitle, validDesc, validSource, validSender, validType, validDate);
            return validNews;
        }
    }
}
