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
        public override async void CompleteAction(object reqObj, NetworkStream stream)
        {
            try
            {
                var convertJsonPerson = JsonConvert.SerializeObject(reqObj);
                var getJsonPerson = JsonConvert.DeserializeObject<Person>(convertJsonPerson); // ПРОБОВАЛ ЧЕРЕЗ НЕЯВНОЕ ПРИВЕДЕНИЕ - НЕ РОБИТ
                var personOutDB = await GetUser(getJsonPerson);
                
                if (getJsonPerson.Password.Equals(personOutDB.Password))
                {
                    var jsonResponsePerson = JsonConvert.SerializeObject(personOutDB);
                    var response = Encoding.UTF8.GetBytes(jsonResponsePerson);
                    await new UserView(response, stream).ExecuteModuleProcessing("Успешно");
                    return;
                }

                if(!getJsonPerson.Password.Equals(personOutDB.Password))
                {
                    var response = Encoding.UTF8.GetBytes("Не найдено ни одного совпадения");
                    await new UserView(response, stream).ExecuteModuleProcessing("Ошибка");

                    serverEvents.BlockReport(this, "Ошибка авторизации. Неверный пароль", ConsoleColor.Red);
                    return;
                }
            }
            catch (NullReferenceException) 
            {
                var response = Encoding.UTF8.GetBytes("Ошибка авторизации");
                await new UserView(response, stream).ExecuteModuleProcessing("");

                serverEvents.BlockReport(this, "Пользователь не найден в базе данных", ConsoleColor.Red);
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
