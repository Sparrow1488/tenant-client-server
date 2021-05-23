using JumboServer.API;
using JumboServer.Functions;
using JumboServer.Meta;
using Newtonsoft.Json;
using System;
using System.Text;

namespace JumboServer.Models.SourceBlock.ADD
{
    public class AddSourceModel
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();

        public byte[] CompleteAction(object reqObject)
        {
            byte[] response = ServerMeta.Encoding.GetBytes("Неизвестная ошибка");
            try
            {
                var source = JsonConvert.DeserializeObject<Source>(reqObject.ToString());
                if(!string.IsNullOrWhiteSpace(source.Data) && source.SenderId > 0)
                {
                    var tokenSource = serverFunctions.InsertSourceInDB(source);
                    if(tokenSource != null)
                        response = ServerMeta.Encoding.GetBytes(tokenSource);
                    else
                        response = ServerMeta.Encoding.GetBytes("Картинка не может быть загружена");
                }

                return response;
            }
            catch (Exception)
            {
                var exMessage = "Неизвестная ошибка";
                serverEvents.BlockReport("AddSource", exMessage, ConsoleColor.Red);
                return Encoding.UTF8.GetBytes(exMessage);
            }
        }
    }
}
