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
            byte[] response;
            try
            {
                var getLetter = JsonConvert.DeserializeObject<Letter>(reqObject.ToString());
                serverEvents.BlockReport(this, "Письмо успешно получено", ConsoleColor.Yellow);
                Console.WriteLine(getLetter); //TODO: сделать сортер по типу новости (предложение, жалоба, вопрос)
                int successInsert = AddLetterInDB(getLetter);
                Console.WriteLine("Успешно добавлено писем: " + successInsert);
                response = Encoding.UTF8.GetBytes("Письмо получено");
                return response;
            }
            catch (Exception) 
            {
                var exMessage = "Неизвестная ошибка";
                serverEvents.BlockReport(this, exMessage, ConsoleColor.Red);
                return Encoding.UTF8.GetBytes(exMessage);
            }
}
        private int AddLetterInDB(Letter newLetter)
        {
            string sCommand = "INSERT INTO [Letters] (Title, Description, Type, Sender, DateCreate, SenderId) VALUES (@title, @desc, @type, @sender, @date, @senderId)";
            using (var command1 = new SqlCommand(sCommand, MyServer.Meta.sqlConnection))
            {
                var validLetter = CheckValidation(newLetter);
                Console.WriteLine("Letter: " + MyServer.Meta.sqlConnection.State);
                command1.Parameters.AddWithValue("title", validLetter.Title);
                command1.Parameters.AddWithValue("desc", validLetter.Description);
                command1.Parameters.AddWithValue("type", validLetter.LetterType);
                command1.Parameters.AddWithValue("sender", validLetter.SenderLogin);
                command1.Parameters.AddWithValue("date", validLetter.DateCreate);
                command1.Parameters.AddWithValue("senderId", validLetter.SenderId);
                var successCount = command1.ExecuteNonQuery();
                return successCount;
            }
        }
        private Letter CheckValidation(Letter letter)
        {
            string validTitle = "", validDesc = "", validType = "";
            DateTime validDate = DateTime.Now;
            string validSender = "noname";
            if (!string.IsNullOrWhiteSpace(letter.Title))
                validTitle = letter.Title;
            if (!string.IsNullOrWhiteSpace(letter.Description))
                validDesc = letter.Description;
            if (!string.IsNullOrWhiteSpace(letter.LetterType))
                validType = letter.LetterType;
            if (!string.IsNullOrWhiteSpace(letter.SenderLogin))
                validSender = letter.SenderLogin;
            var validLetter = new Letter(validTitle, validDesc, validSender, validType, validDate, letter.Id, letter.SenderId);
            return validLetter;
        }
    }
}
