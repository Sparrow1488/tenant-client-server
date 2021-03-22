using JumboServer.Meta;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using JumboServer.Functions;
using JumboServer.API;

namespace JumboServer.Models.SourceBlock.ADD
{
    public class AddSourceModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public AddSourceModel(string modelAction, bool forOnlyAdmin) : base(modelAction, forOnlyAdmin) { }

        public override byte[] CompleteAction(object reqObject)
        {
            byte[] response = ServerMeta.Encoding.GetBytes("Неизвестная ошибка");
            try
            {
                var source = JsonConvert.DeserializeObject<Source>(reqObject.ToString());
                if(!string.IsNullOrWhiteSpace(source.Data) && source.SenderId > 0)
                {
                    var tokenSource = serverFunctions.InsertSourceInDB(source);
                    if(tokenSource != null)
                    {
                        serverEvents.BlockReport(this, "Контент успешно добавлен в БД", ConsoleColor.Green);
                        response = ServerMeta.Encoding.GetBytes(tokenSource);
                    }
                    else
                    {
                        serverEvents.BlockReport(this, "Ошибка добавления картинки в БД", ConsoleColor.Yellow);
                        response = ServerMeta.Encoding.GetBytes("Картинка не может быть загружена");
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
