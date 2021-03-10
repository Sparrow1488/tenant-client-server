using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Blocks.LetterBlock;
using Multi_Server_Test.Server.Models.AuthBlock;
using Multi_Server_Test.Server.Models.LetterBlock;
using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;

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
                        int id = reader.GetInt32(0);
                        string title = reader.GetString(1);
                        string description = reader.GetString(2);
                        string source = reader.IsDBNull(3) ? null : reader.GetString(3);
                        DateTime date = reader.IsDBNull(4) ? DateTime.Today : reader.GetDateTime(4);
                        string type = reader.IsDBNull(6) ? null : reader.GetString(6);
                        int senderId = reader.GetInt32(7);
                        string sender = GetUserLoginById(senderId);
                        var news = new News(id, title, description, source, sender, type, date, senderId);
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
                        var date = reader.GetDateTime(5);
                        var senderId = reader.GetInt32(6);
                        var sender = GetUserLoginById(senderId);

                        selectLetters.Add(new Letter(title, desc, sender, type, date, id, senderId));
                    }
                }
                reader.Close();
            }
            return selectLetters;
        }
        public string GetUserLoginById(int id)
        {
            foreach (var user in MyServer.allUsers)
            {
                if (user.Id == id)
                    return user.Login;
            }
            return "delited";
        }
        public int InsertNewsInDB(News news)
        {
            try
            {
                string sCommand = "INSERT INTO [News] (Title, Description, DateCreate, Sender, Type, SenderId) VALUES (@title, @desc, @date, @sender, @type, @senderId)";
                using (var command = new SqlCommand(sCommand, MyServer.Meta.sqlConnection))
                {
                    var validNews = CheckNewsValidation(news);
                    command.Parameters.AddWithValue("title",  validNews.Title);
                    command.Parameters.AddWithValue("desc",   validNews.Description);
                    command.Parameters.AddWithValue("date",   validNews.DateTime);
                    command.Parameters.AddWithValue("sender", "");
                    command.Parameters.AddWithValue("type",   validNews.Type);
                    command.Parameters.AddWithValue("senderId",   validNews.SenderId);
                    var successInsert = command.ExecuteNonQuery();
                    return successInsert;
                }
            }
            catch (Exception) { return -1; }
        }
        public ReplyLetter GetReplyByLetterId(int id)
        {
            string sCommand = $"SELECT * FROM ResponsesToLetters WHERE letterId={id}";
            var command = new SqlCommand(sCommand, MyServer.Meta.sqlConnection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var answer = reader.GetString(1);
                        var letterId = reader.GetInt32(2);
                        var source = reader.IsDBNull(3) ? null : reader.GetString(3);
                        var responder = reader.GetString(4);
                        return new ReplyLetter(answer, source, responder, letterId);
                    }
                }
                reader.Close();
            }
            return null;
        }
        private News CheckNewsValidation(News checkNews) //каловая дичь
        {
            string validTitle = "", validDesc = "", validSource = "", validType = "notice";
            DateTime validDate = DateTime.Now;
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
            var validNews = new News(checkNews.Id, validTitle, validDesc, validSource, validSender, validType, validDate, checkNews.SenderId);
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
                        var id = reader.GetInt32(0);
                        var login = reader.GetString(1);
                        var password = reader.GetString(2);
                        var name = reader.GetString(3);
                        var lastName = reader.GetString(4);
                        var parentName = reader.GetString(5);
                        var roomNum = Convert.ToInt32(reader.GetValue(6));
                        return new Person(name, lastName, parentName, login, password, roomNum, id, null);
                    }
                }
                reader.Close();
            }
            return null;
        }
        public string GetUserLoginOrDefault(int userId)
        {
            string sCommand = $"SELECT * FROM Users WHERE Id={userId}";
            var command = new SqlCommand(sCommand, MyServer.Meta.sqlConnection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var login = reader.GetString(1);
                        return login;
                    }
                }
                reader.Close();
            }
            return "noname";
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

        public List<Letter> GetAllLetterByUserId(int id)
        {
            var collection = new List<Letter>();
            foreach (var letter in MyServer.allLetters)
            {
                if (letter.SenderId == id)
                    collection.Add(letter);
            }
            return collection;
        }
        public int ReplyToTheLetter(ReplyLetter reply)
        {
            string sCommand = $"INSERT INTO [ResponsesToLetters] (answerText, letterId, source, responder) VALUES (@answer, @letterId, @source, @sender)";
            using (var command = new SqlCommand(sCommand, MyServer.Meta.sqlConnection))
            {
                command.Parameters.AddWithValue("answer", reply.Answer);
                command.Parameters.AddWithValue("letterId", reply.LetterId);
                string validSource = "";
                if (reply.Source != null) //TODO: попробовать при != null добавлять параметр, иначе нет
                    validSource = reply.Source;
                command.Parameters.AddWithValue("source", validSource); 
                command.Parameters.AddWithValue("sender", reply.Responder);
                var successInsert = command.ExecuteNonQuery();
                return successInsert;
            }
        }
        public List<Person> GetAllUsersOutDB()
        {
            var usersCollection = new List<Person>();
            string sCommand = $"SELECT * FROM Users";
            var command = new SqlCommand(sCommand, MyServer.Meta.sqlConnection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var login = reader.GetString(1);
                        var password = reader.GetString(2);
                        var name = reader.GetString(3);
                        var lastName = reader.GetString(4);
                        var parentName = reader.GetString(5);
                        var roomNum = Convert.ToInt32(reader.GetValue(6));
                        usersCollection.Add(new Person(name, lastName, parentName, login, password, roomNum, id, null));
                    }
                }
                reader.Close();
            }
            return usersCollection;
        }
        public int InsertImageInDB(string base64Image)
        {
            try
            {
                string sCommand = "INSERT INTO [Sources] (Data, Type, SenderId, DateCreate) VALUES (@data, @type, @senderId, @date)";
                using (var command = new SqlCommand(sCommand, MyServer.Meta.sqlConnection))
                {
                    command.Parameters.AddWithValue("data", base64Image);
                    command.Parameters.AddWithValue("type", ".jpg");
                    command.Parameters.AddWithValue("senderId", -1);
                    command.Parameters.AddWithValue("date", DateTime.Now);
                    var successInsert = command.ExecuteNonQuery();
                    return successInsert;
                }
            }
            catch (Exception) { return -1; }
        }
        public string GetImageBase64OutDB(int imageId)
        {
            string sCommand = $"SELECT * FROM Sources WHERE id={imageId}";
            var command = new SqlCommand(sCommand, MyServer.Meta.sqlConnection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var data = reader.GetString(1);
                        return data;
                    }
                }
                reader.Close();
            }
            return "nodata";
        }
    }
}
