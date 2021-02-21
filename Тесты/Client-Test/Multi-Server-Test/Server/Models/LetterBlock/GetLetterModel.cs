using Multi_Server_Test.Server.Views;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Server_Test.Server.Models.LetterBlock
{
    public class GetLetterModel : Model
    {
        private ServerModulEvents serverEvents = new ServerModulEvents();
        public GetLetterModel(string modelAction) : base(modelAction) { }

        public override async Task<byte[]> CompleteAction(object reqObject)
        {
            try
            {
                serverEvents.BlockReport(this, "Запрос на получение новостей", ConsoleColor.Green);
                var lettersOutDB = GetLettersCollection();
                byte[] response;

                if (lettersOutDB == null)
                {
                    response = Encoding.UTF8.GetBytes("Список писем пока пуст");
                    //await new LetterView(response, stream).ExecuteModuleProcessing("");
                }
                else
                {
                    string responseLetters = JsonConvert.SerializeObject(lettersOutDB);
                    response = Encoding.UTF8.GetBytes(responseLetters);
                    //await new LetterView(response, stream).ExecuteModuleProcessing("");
                }
                return response;
            }
            catch (Exception) 
            {
                return Encoding.UTF8.GetBytes("Неизвестная ошибка");
            }
        }
        private LettersCollection GetLettersCollection()
        {
            return MyServer.allLetters; // TODO: сделать дичь с доставанием новостей с сервера
        }
    }
}
