using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Models.SourceBlock
{
    public class AddSourceModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public AddSourceModel(string modelAction) : base(modelAction) { }

        public override byte[] CompleteAction(object reqObject)
        {
            byte[] response = Encoding.UTF8.GetBytes("Неизвестная ошибка");
            try
            {
                var source = JsonConvert.DeserializeObject<Source>(reqObject.ToString());
                if(!string.IsNullOrWhiteSpace(source.Data) && source.SenderId > 0)
                {
                    var tokenSource = serverFunctions.InsertImageInDB(source);
                    if(tokenSource != null)
                    {
                        serverEvents.BlockReport(this, "Контент успешно добавлен в БД", ConsoleColor.Green);
                        response = Encoding.UTF8.GetBytes(tokenSource);
                    }
                    else
                    {
                        serverEvents.BlockReport(this, "Ошибка добавления картинки в БД", ConsoleColor.Yellow);
                        response = Encoding.UTF8.GetBytes("Картинка не может быть загружена");
                    }
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
    }
}
