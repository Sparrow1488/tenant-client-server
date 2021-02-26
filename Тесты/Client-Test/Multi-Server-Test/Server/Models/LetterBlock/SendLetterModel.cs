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
        private ServerModulEvents serverEvents = new ServerModulEvents();
        public SendLetterModel(string modelAction) : base(modelAction) { }
        public override byte[] CompleteAction(object reqObject)
        {
            byte[] response;
            try
            {
                var getJsonLetter = JsonConvert.SerializeObject(reqObject);
                var getLetter = JsonConvert.DeserializeObject<Letter>(getJsonLetter);
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
            string sCommand = "INSERT INTO [Letters] (Title, Description, Type, Sender) VALUES (@title, @desc, @type, @sender)";
            using (var command1 = new SqlCommand(sCommand, MyServer.Meta.sqlConnection))
            {
                Console.WriteLine(MyServer.Meta.sqlConnection.State);
                command1.Parameters.AddWithValue("title", newLetter.Title);
                command1.Parameters.AddWithValue("desc", newLetter.Description);
                command1.Parameters.AddWithValue("type", newLetter.LetterType);
                command1.Parameters.AddWithValue("sender", newLetter.SenderLogin);
                var successCount = command1.ExecuteNonQuery();
                return successCount;
            }
        }
    }
}
