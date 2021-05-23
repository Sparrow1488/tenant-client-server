using JumboServer.Meta;
using Newtonsoft.Json;
using System;
using System.Text;
using JumboServer.Functions;

namespace JumboServer.Models.SourceBlock.GET
{
    public class GetSourceModel
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();

        public byte[] CompleteAction(object reqObject)
        {
            byte[] response = ServerMeta.Encoding.GetBytes("Неизвестная ошибка");
            try
            {
                var tokenRequest = Convert.ToString(reqObject);
                var source = serverFunctions.GetSourceByTokenOutDB(tokenRequest);
                if(source != null)
                {
                    response = ServerMeta.Encoding.GetBytes(JsonConvert.SerializeObject(source));
                }
                return response;
            }
            catch (Exception)
            {
                var exMessage = "Неизвестная ошибка";
                serverEvents.BlockReport("GetSource", exMessage, ConsoleColor.Red);
                return ServerMeta.Encoding.GetBytes(exMessage);
            }
        }
    }
}
