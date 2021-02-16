using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Blocks;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

namespace Multi_Server_Test.ServerData.Blocks.Auth
{
    public class AuthorizationBlock : ServerBlock
    {
        public AuthorizationBlock(string blockAction, MyServer server) : base(blockAction, server) { }

        public override async void CompleteAction(string clientJson, NetworkStream stream)
        {
            try
            {
                var getClientPerson = JsonConvert.DeserializeObject<Person>(clientJson);
                var personOutDB = await RequestServer.GetUser(getClientPerson);
                
                if (getClientPerson.Password.Equals(personOutDB.Password))
                {
                    var jsonResponsePerson = JsonConvert.SerializeObject(personOutDB);
                    var response = Encoding.UTF8.GetBytes(jsonResponsePerson);
                    if (stream.CanWrite)
                    {
                        await stream.WriteAsync(response, 0, response.Length);
                        BlockReport("Успешный вход", ConsoleColor.Green);
                    }
                    else
                    {
                        BlockReport("Ошибка записи потока: не поддерживается запись", ConsoleColor.Yellow);
                    }
                    return;
                }

                if(!getClientPerson.Password.Equals(personOutDB.Password))
                {
                    var response = Encoding.UTF8.GetBytes("Не найдено ни одного совпадения");
                    await stream.WriteAsync(response, 0, response.Length);

                    BlockReport("Ошибка авторизации. Неверный пароль", ConsoleColor.Red);
                    return;
                }
            }
            catch (NullReferenceException) 
            {
                var response = Encoding.UTF8.GetBytes("Ошибка авторизации");
                await stream.WriteAsync(response, 0, response.Length);

                BlockReport("Пользователь не найден в базе данных", ConsoleColor.Red);
            }
            finally
            {

            }
        }

        private void BlockReport(string message, ConsoleColor color)
        {
            Console.Write(BlockAction + "> ");

            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
