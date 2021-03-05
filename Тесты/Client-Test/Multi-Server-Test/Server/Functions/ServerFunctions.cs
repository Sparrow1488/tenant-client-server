using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Blocks.LetterBlock;
using Multi_Server_Test.Server.Models.AuthBlock;
using Multi_Server_Test.Server.Models.LetterBlock;
using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Multi_Server_Test.Server.Functions
{
    public class ServerFunctions
    {
        public List<News> GetAllNewsOutDB()
        {
            var listNews = new List<News>();

            var command = new SqlCommand("SELECT * FROM News", MyServer.Meta.sqlConnection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string title = reader.GetString(1);
                        string description = reader.GetString(2);
                        string source = reader.IsDBNull(3) ? null : reader.GetString(3);
                        DateTime date = reader.IsDBNull(4) ? DateTime.Today : reader.GetDateTime(4);
                        string sender = reader.IsDBNull(5) ? null : reader.GetString(5);
                        string type = reader.IsDBNull(6) ? null : reader.GetString(6);
                        var news = new News(title, description, source, sender, type, date);
                        listNews.Add(news);
                    }
                    return listNews;
                }
                return null;
            }
        }
        public List<Letter> GetAllLettersOutDB()
        {
            var selectLetters = new List<Letter>();
            string sCommand = "SELECT * FROM Letters";
            var command = new SqlCommand(sCommand, MyServer.Meta.sqlConnection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var title = reader.GetString(1);
                        var desc = reader.GetString(2);
                        var type = reader.GetString(3);
                        var sender = reader.GetString(4);
                        var date = reader.GetDateTime(5);
                        selectLetters.Add(new Letter(title, desc, sender, type, date, id));
                    }
                }
                reader.Close();
            }
            return selectLetters;
        }
        public int InsertNewsInDB(News news)
        {
            try
            {
                string sCommand = "INSERT INTO [News] (Title, Description, DateCreate, Sender, Type) VALUES (@title, @desc, @date, @sender, @type)";
                using (var command = new SqlCommand(sCommand, MyServer.Meta.sqlConnection))
                {
                    var validNews = CheckNewsValidation(news);
                    command.Parameters.AddWithValue("title",  validNews.Title);
                    command.Parameters.AddWithValue("desc",   validNews.Description);
                    command.Parameters.AddWithValue("date",   validNews.DateTime);
                    command.Parameters.AddWithValue("sender", validNews.Sender);
                    command.Parameters.AddWithValue("type",   validNews.Type);
                    var successInsert = command.ExecuteNonQuery();
                    MyServer.newsCollectionOutDB.Add(validNews);
                    return successInsert;
                }
            }
            catch (Exception) { return -1; }
        }
        private News CheckNewsValidation(News checkNews)
        {
            string validTitle = "", validDesc = "", validSource = "", validType = "";
            DateTime validDate = Convert.ToDateTime("1/1/1900 12:00:00 ");
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
        public Person GetUserOrDefault(Person person)
        {
            string sCommand = $"SELECT * FROM Users WHERE Login=N'{person.Login}' AND Password=N'{person.Password}'";
            var command = new SqlCommand(sCommand, MyServer.Meta.sqlConnection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var login = reader.GetString(1);
                        var password = reader.GetString(2);
                        var name = reader.GetString(3);
                        var lastName = reader.GetString(4);
                        var parentName = reader.GetString(5);
                        var roomNum = Convert.ToInt32(reader.GetValue(6));
                        return new Person(name, lastName, parentName, login, password, roomNum, null);
                    }
                }
                reader.Close();
            }
            return null;
        }
        public Person GetUserByTokenOrDefault(UserToken token)
        {
            foreach (var item in MyServer.tokensDictionary)
            {
                if (UserTokenIsExist(token) &&
                    item.Key.SynchronizationPassword == token.SynchronizationPassword &&
                    item.Key.EncodedLogin == token.EncodedLogin)
                    return item.Value;
            }
            return null;
        }
        public UserToken GetUserTokenOrDefault(Person user)
        {
            foreach (var item in MyServer.tokensDictionary)
            {
                if (item.Value.Login == user.Login &&
                    item.Value.Password == user.Password)
                    return item.Key;
            }
            return null;
        }
        public bool UserTokenIsExist(Person user)
        {
            foreach (var item in MyServer.tokensDictionary)
            {
                if (item.Value.Login == user.Login &&
                    item.Value.Password == user.Password)
                    return true;
            }
            return false;
        }
        public bool UserTokenIsExist(UserToken token)
        {
            foreach (var item in MyServer.tokensDictionary)
            {
                if (item.Key.SynchronizationPassword == token.SynchronizationPassword &&
                    item.Key.EncodedLogin == token.EncodedLogin)
                    return true;
            }
            return false;
        }
        public int ReplyToTheLetter(ReplyLetter reply)
        {
            string sCommand = $"INSERT INTO [ResponsesToLetters] (answerText, letterId, source, responder) VALUES (@answer, @letterId, @source, @sender)";
            using (var command = new SqlCommand(sCommand, MyServer.Meta.sqlConnection))
            {
                command.Parameters.AddWithValue("answer", reply.Answer);
                command.Parameters.AddWithValue("letterId", reply.LetterId);
                string validSource = "";
                if (reply.Source != null)
                    validSource = reply.Source;
                command.Parameters.AddWithValue("source", validSource);
                command.Parameters.AddWithValue("sender", reply.Responder);
                var successInsert = command.ExecuteNonQuery();
                return successInsert;
            }
        }
        //private ReplyLetter CheckAnswerValidation(ReplyLetter answer)
        //{
        //    ReplyLetter validReply;
        //    string validAnswer;
        //    int letterId;
        //    string validSender;
        //    if (!string.IsNullOrWhiteSpace(answer.Answer))
        //        validAnswer = answer
        //}
    }
}
