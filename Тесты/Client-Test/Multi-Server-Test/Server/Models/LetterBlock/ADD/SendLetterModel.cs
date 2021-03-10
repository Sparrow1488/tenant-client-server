using Multi_Server_Test.Blocks;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Text;

namespace Multi_Server_Test.Server.Blocks.LetterBlock
{
    public class SendLetterModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        public SendLetterModel(string modelAction) : base(modelAction) { }
        public override byte[] CompleteAction(object reqObject)
        {
            byte[] response = Encoding.UTF8.GetBytes("Неизвестная ошибка");
            try
            {
                var getLetter = JsonConvert.DeserializeObject<Letter>(reqObject.ToString());
                serverEvents.BlockReport(this, "Письмо успешно получено", ConsoleColor.Yellow);
                Console.WriteLine(getLetter);

                MyServer.allLetters.Add(getLetter);
                MyServer.noSynchLetters.Add(getLetter);

                var canInsertInDB = CheckValidation(getLetter);

                if (canInsertInDB)
                {
                    int successInsert = AddLetterInDB(getLetter);
                    serverEvents.BlockReport(this, "Успешно добавлено писем: " + successInsert, ConsoleColor.Green);
                    response = Encoding.UTF8.GetBytes("Письмо получено");
                }
                else
                {
                    serverEvents.BlockReport(this, "Ошибка валидации полученного письма: не добавлено в БД", ConsoleColor.Yellow);
                    response = Encoding.UTF8.GetBytes("Ошибка валидации");
                }
                
                return response;
            }
            catch (Exception)
            {
                var exMessage = "Неизвестная ошибка";
                serverEvents.BlockReport(this, exMessage, ConsoleColor.Red);
                return Encoding.UTF8.GetBytes(exMessage);
            }
        }
        private bool CheckValidation(Letter letter)
        {
            if (string.IsNullOrEmpty(letter.Title) || string.IsNullOrEmpty(letter.Description))
            {
                return false;
            }
            return true;
        }
        private int AddLetterInDB(Letter newLetter)
        {
            string sCommand = "INSERT INTO [Letters] (Title, Description, Type, DateCreate, SenderId) VALUES (@title, @desc, @type, @date, @senderId)";
            using (var command1 = new SqlCommand(sCommand, MyServer.Meta.sqlConnection))
            {
                var validLetter = newLetter; //TODO: сделать валидацию письма
                Console.WriteLine("Letter: " + MyServer.Meta.sqlConnection.State);
                command1.Parameters.AddWithValue("title", validLetter.Title);
                command1.Parameters.AddWithValue("desc", validLetter.Description);
                command1.Parameters.AddWithValue("type", validLetter.LetterType);
                command1.Parameters.AddWithValue("date", validLetter.DateCreate);
                command1.Parameters.AddWithValue("senderId", validLetter.SenderId);
                var successCount = command1.ExecuteNonQuery();
                return successCount;
            }
        }
    }
}
