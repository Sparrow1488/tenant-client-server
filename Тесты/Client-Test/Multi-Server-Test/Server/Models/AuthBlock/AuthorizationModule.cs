﻿using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Functions;
using Multi_Server_Test.Server.Models.AuthBlock;
using Multi_Server_Test.ServerData.Server;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Text;

namespace Multi_Server_Test.ServerData.Blocks.Auth
{
    public class AuthorizationModel : Model
    {
        public AuthorizationModel(string modelAction, bool forOnlyAdmin) : base(modelAction, forOnlyAdmin) { }
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();
        public override byte[] CompleteAction(object reqObj)
        {
            try
            {
                var getInputPersonData = JsonConvert.DeserializeObject<Person>(reqObj.ToString());
                var authorizatePerson = serverFunctions.GetUserOrDefaultOutDB(getInputPersonData);

                if (authorizatePerson != null)
                {
                    var userToken = serverFunctions.GenerateToken(authorizatePerson);
                    authorizatePerson.Token = userToken;
                    MyServer.tokensDictionary.Add(userToken, authorizatePerson);

                    var jsonResponsePerson = JsonConvert.SerializeObject(authorizatePerson);
                    byte[] response = ServerMeta.Encoding.GetBytes(jsonResponsePerson);
                    serverEvents.BlockReport(this, "Успешный вход", ConsoleColor.Green);
                    return response;
                }
                else
                {
                    byte[] response = ServerMeta.Encoding.GetBytes("Не найдено ни одного совпадения");
                    serverEvents.BlockReport(this, "Ошибка авторизации. Неверный пароль", ConsoleColor.Red);
                    return response;
                }
            }
            catch (NullReferenceException)
            {
                byte[] response = ServerMeta.Encoding.GetBytes("Ошибка авторизации");
                serverEvents.BlockReport(this, "Пользователь не найден в базе данных", ConsoleColor.Red);
                return response;
            }
        }
    }
}
