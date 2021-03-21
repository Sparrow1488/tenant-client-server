﻿using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Server;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Multi_Server_Test.Server.Models.AuthBlock
{
    public class AuthorizationForTokenModel : Model
    {
        public AuthorizationForTokenModel(string modelAction, bool forOnlyAdmin) : base(modelAction, forOnlyAdmin) { }
        private ServerFunctions serverFunctions = new ServerFunctions();
        private ServerReportsModule serverEvents = new ServerReportsModule();

        public override byte[] CompleteAction(object reqObject)
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
                serverEvents.BlockReport(this, "Успешный вход по токену", ConsoleColor.Green);
                return response;
            }
            else
            {
                response = ServerMeta.Encoding.GetBytes("Ошибка входа через токен. Попробуйте авторизацию по логину и паролю");
                serverEvents.BlockReport(this, "Ошибка входа по токену", ConsoleColor.Green);
                return response;
            }
        }
    }
}
