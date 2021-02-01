﻿using Multi_Server_Test.Blocks;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Server_Test.ServerData.Blocks.Auth
{
    public class AuthorizationBlock : ServerBlock
    {
        public AuthorizationBlock(string blockAction) : base(blockAction) { }

        public override async Task CompleteAction(string clientJson, NetworkStream stream)
        {
            try
            {
                var getClientPerson = JsonConvert.DeserializeObject<Person>(clientJson);
                var personOutDB = await ServerMethods.GetUserOutDB(getClientPerson);
                
                if (getClientPerson.Password.Equals(personOutDB.Password))
                {
                    var sendPerson = JsonConvert.SerializeObject(personOutDB);
                    var response = Encoding.UTF8.GetBytes(sendPerson);
                    await stream.WriteAsync(response, 0, response.Length);
                    Console.WriteLine("Успешный вход");
                }
                else
                {
                    var response = Encoding.UTF8.GetBytes("Возможно, Вы ввели неверный пароль!");
                    await stream.WriteAsync(response, 0, response.Length);
                    Console.WriteLine("Ошибка авторизации");
                }
            }
            catch (NullReferenceException) 
            {
                Console.WriteLine("Ошибка авторизации");
                var response = Encoding.UTF8.GetBytes("Ошибка авторизации");
                await stream.WriteAsync(response, 0, response.Length);
                Console.WriteLine("Ответ отправлен");
            }
        }
    }
}
