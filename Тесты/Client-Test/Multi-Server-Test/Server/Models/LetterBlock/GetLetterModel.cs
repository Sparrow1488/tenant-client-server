using Multi_Server_Test.Server.Blocks.LetterBlock;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Multi_Server_Test.Server.Models.LetterBlock
{
    public class GetLetterModel : Model
    {
        private ServerModulEvents serverEvents = new ServerModulEvents();
        public GetLetterModel(string modelAction) : base(modelAction) { }

        public override byte[] CompleteAction(object reqObject)
        {
            try
            {
                byte[] response;
                serverEvents.BlockReport(this, "Запрос на получение новостей", ConsoleColor.Green);
                List<Letter> lettersOutDB = GetLettersCollection();

                if (lettersOutDB == null)
                    response = Encoding.UTF8.GetBytes("Список писем пока пуст");
                else
                {
                    string responseLetters = JsonConvert.SerializeObject(lettersOutDB);
                    response = Encoding.UTF8.GetBytes(responseLetters);
                }
                return response;
            }
            catch (Exception) 
            {
                return Encoding.UTF8.GetBytes("Неизвестная ошибка");
            }
        }
        private List<Letter> GetLettersCollection()
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
                        var title = reader.GetString(1);
                        var desc = reader.GetString(2);
                        var type = reader.GetString(3);
                        var sender = reader.GetString(4);
                        var date = reader.GetDateTime(5);
                        selectLetters.Add(new Letter(title, desc, sender, type, date));
                    }
                }
                reader.Close();
            }
            return selectLetters;
        }
    }
}
