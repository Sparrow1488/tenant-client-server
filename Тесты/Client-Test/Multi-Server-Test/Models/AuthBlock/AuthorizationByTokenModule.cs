using JumboServer.Meta;
using Newtonsoft.Json;
using System;
using JumboServer.Functions;
using JumboServer.API;

namespace JumboServer.Models.Authorization
{
    public class AuthorizationByTokenModule
    {
        private ServerFunctions serverFunctions = new ServerFunctions();
        private ServerReportsModule serverEvents = new ServerReportsModule();

        public byte[] CompleteAction(object reqObject)
        {
            byte[] response = ServerMeta.Encoding.GetBytes("-1");
            if (reqObject == null)
                return response;
            var getInputToken = JsonConvert.DeserializeObject<UserToken>(reqObject?.ToString()); //TODO: иногда null вылетает
            var authUser = serverFunctions.GetUserByTokenOrDefault(getInputToken);

            if(authUser != null)
            {
                var jsonResponsePerson = JsonConvert.SerializeObject(authUser);
                response = ServerMeta.Encoding.GetBytes(jsonResponsePerson);
                return response;
            }
            else
            {
                response = ServerMeta.Encoding.GetBytes("Ошибка входа через токен. Попробуйте авторизацию по логину и паролю");
                serverEvents.BlockReport("AuthToken", "Ошибка входа по токену", ConsoleColor.Green);
                return response;
            }
        }
    }
}
