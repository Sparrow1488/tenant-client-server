using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Server;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Multi_Server_Test.Server.Models.SourceBlock
{
    public class GetSourceModel : Model
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public GetSourceModel(string modelAction, bool forOnlyAdmin) : base(modelAction, forOnlyAdmin) { }

        public override byte[] CompleteAction(object reqObject)
        {
            byte[] response = ServerMeta.Encoding.GetBytes("Неизвестная ошибка");
            try
            {
                var tokenRequest = Convert.ToString(reqObject);
                var source = serverFunctions.GetSourceByTokenOutDB(tokenRequest);
                if(source != null)
                {
                    serverEvents.BlockReport(this, "Получен контент", ConsoleColor.Yellow);
                    response = ServerMeta.Encoding.GetBytes(JsonConvert.SerializeObject(source));
                }
                return response;
            }
            catch (Exception)
            {
                var exMessage = "Неизвестная ошибка";
                serverEvents.BlockReport(this, exMessage, ConsoleColor.Red);
                return ServerMeta.Encoding.GetBytes(exMessage);
            }
        }
    }
}
