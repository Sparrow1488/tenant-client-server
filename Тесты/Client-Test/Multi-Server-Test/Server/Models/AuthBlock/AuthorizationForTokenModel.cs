using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Multi_Server_Test.Server.Models.AuthBlock
{
    public class AuthorizationForTokenModel : Model
    {
        public AuthorizationForTokenModel(string modelAction) : base(modelAction) { }
        private ServerFunctions serverFunctions = new ServerFunctions();
        private ServerReportsModule serverEvents = new ServerReportsModule();

        public override byte[] CompleteAction(object reqObject)
        {
            byte[] response = Encoding.UTF8.GetBytes("-1");
            if (reqObject == null)
                return response;
            var getInputToken = JsonConvert.DeserializeObject<UserToken>(reqObject?.ToString()); //TODO: иногда null вылетает
            var authUser = serverFunctions.GetUserByTokenOrDefault(getInputToken);

            if(authUser != null)
            {
                var jsonResponsePerson = JsonConvert.SerializeObject(authUser);
                response = Encoding.UTF8.GetBytes(jsonResponsePerson);
                serverEvents.BlockReport(this, "Успешный вход по токену", ConsoleColor.Green);
                return response;
            }
            else
            {
                response = Encoding.UTF8.GetBytes("Ошибка входа через токен. Попробуйте авторизацию по логину и паролю");
                serverEvents.BlockReport(this, "Ошибка входа по токену", ConsoleColor.Green);
                return response;
            }
        }
    }
}
