using JumboServer;
using JumboServer.Meta;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Text;
using JumboServer.Functions;
using JumboServer.API;

namespace JumboServer.Models.Authorization
{
    public class AuthorizationModel
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        private ServerFunctions serverFunctions = new ServerFunctions();


        public byte[] CompleteAction(object reqObj)
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
                    return response;
                }
                else
                {
                    byte[] response = ServerMeta.Encoding.GetBytes("Не найдено ни одного совпадения");
                    return response;
                }
            }
            catch (NullReferenceException)
            {
                byte[] response = ServerMeta.Encoding.GetBytes("Ошибка авторизации");
                serverEvents.BlockReport("Auth", "Пользователь не найден в базе данных", ConsoleColor.Red);
                return response;
            }
        }
    }
}
