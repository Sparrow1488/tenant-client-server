using Multi_Server_Test.Blocks;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Text;

namespace Multi_Server_Test.ServerData.Blocks.Auth
{
    public class AuthorizationModel : Model
    {
        public AuthorizationModel(string blockAction) : base(blockAction) { }
        private ServerModulEvents serverEvents = new ServerModulEvents();
        public override byte[] CompleteAction(object reqObj)
        {
            try
            {
                var convertJsonPerson = JsonConvert.SerializeObject(reqObj);
                var getInputPersonData = JsonConvert.DeserializeObject<Person>(convertJsonPerson); // ПРОБОВАЛ ЧЕРЕЗ НЕЯВНОЕ ПРИВЕДЕНИЕ - НЕ РОБИТ
                var authorizatePerson = GetAndAuthUser(getInputPersonData);

                if (authorizatePerson != null)
                {
                    var jsonResponsePerson = JsonConvert.SerializeObject(authorizatePerson);
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
            }
        }
        private Person GetAndAuthUser(Person person)
        {
            string sCommand = $"SELECT * FROM Users WHERE Login=N'{person.Login}' AND Password=N'{person.Password}'";
            var command = new SqlCommand(sCommand, MyServer.Meta.sqlConnection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var login = reader.GetString(1);
                        var password = reader.GetString(2);
                        var name = reader.GetString(3);
                        var lastName = reader.GetString(4);
                        var parentName = reader.GetString(5);
                        var roomNum = Convert.ToInt32(reader.GetValue(6));
                        return new Person(name, lastName, parentName, login, password, roomNum);
                    }
                }
                reader.Close();
            }
            return null;
        }
    }
}
