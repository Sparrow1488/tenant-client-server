using Multi_Server_Test.Server.Views;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

namespace Multi_Server_Test.Server.Models.LetterBlock
{
    public class GetLetterModel : Model
    {
        private ServerModulEvents serverEvents = new ServerModulEvents();
        public GetLetterModel(string modelAction) : base(modelAction) { }

        public override async void CompleteAction(object reqObject, NetworkStream stream)
        {
            try
            {
                serverEvents.BlockReport(this, "Запрос на получение новостей", ConsoleColor.Green);
                var lettersOutDB = GetLettersCollection();

                if (lettersOutDB == null)
                {
                    var response = Encoding.UTF8.GetBytes("Список писем пока пуст");
                    await new LetterView(response, stream).ExecuteModuleProcessing("");
                }
                else
                {
                    string responseLetters = JsonConvert.SerializeObject(lettersOutDB);
                    var response = Encoding.UTF8.GetBytes(responseLetters);
                    await new LetterView(response, stream).ExecuteModuleProcessing("");
                }
            }
            catch (Exception) { }
        }
        private LettersCollection GetLettersCollection()
        {
            return MyServer.allLetters; // TODO: сделать дичь с доставанием новостей с сервера
        }
    }
}
