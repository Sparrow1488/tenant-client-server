using FireSharp;
using FireSharp.Response;
using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Views;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Server_Test.ServerData.Blocks.Auth
{
    public class AuthorizationModel : Model
    {
        public AuthorizationModel(string blockAction) : base(blockAction) { }
        private ServerModulEvents serverEvents = new ServerModulEvents();
        public override async Task<byte[]>CompleteAction(object reqObj)
        {
            try
            {
                var convertJsonPerson = JsonConvert.SerializeObject(reqObj);
                var getJsonPerson = JsonConvert.DeserializeObject<Person>(convertJsonPerson); // ПРОБОВАЛ ЧЕРЕЗ НЕЯВНОЕ ПРИВЕДЕНИЕ - НЕ РОБИТ
                var personOutDB = await GetUser(getJsonPerson);
                
                if (getJsonPerson.Password.Equals(personOutDB.Password))
                {
                    var jsonResponsePerson = JsonConvert.SerializeObject(personOutDB);
                    byte[] response = Encoding.UTF8.GetBytes(jsonResponsePerson);
                    serverEvents.BlockReport(this, "Успешный вход", ConsoleColor.Green);
                    return response;
                }
                else
                {
                    byte[] response = Encoding.UTF8.GetBytes("Не найдено ни одного совпадения");
                    serverEvents.BlockReport(this, "Ошибка авторизации. Неверный пароль", ConsoleColor.Red);
                    return response;
                }
            }
            catch (NullReferenceException)
            {
                byte[] response = Encoding.UTF8.GetBytes("Ошибка авторизации");
                serverEvents.BlockReport(this, "Пользователь не найден в базе данных", ConsoleColor.Red);
                return response;
                //await new UserView(response, stream).ExecuteModuleProcessing("");
            }
        }
        private async Task<Person> GetUser(Person person)
        {
            FirebaseResponse respose;
            try
            {
                MyServer.Meta.firebaseClient = new FirebaseClient(MyServer.Meta.firebaseConfig);
                respose = await MyServer.Meta.firebaseClient.GetAsync($"{MyServer.Meta.usersPath}/{person.Login}");
            }
            catch (NullReferenceException) { return null; }

            var user = respose.ResultAs<Person>();
            return user;
        }
    }
}
