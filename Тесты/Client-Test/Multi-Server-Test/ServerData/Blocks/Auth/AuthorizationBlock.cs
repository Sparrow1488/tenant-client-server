using Multi_Server_Test.Blocks;
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
                var getPerson = JsonConvert.DeserializeObject<Person>(clientJson);

                var wasPerson = await ServerMethods.GetUserOutDB(getPerson);
                if (wasPerson.Equals(null))
                {
                    await ServerMethods.AddInDb(getPerson);
                    Console.WriteLine("Пользователь создан");

                    var response = Encoding.UTF8.GetBytes("");
                    await stream.WriteAsync(response, 0, response.Length);
                    Console.WriteLine("Ответ отправлен");
                }
                else
                {
                    var sendPerson = JsonConvert.SerializeObject(wasPerson);
                    var response = Encoding.UTF8.GetBytes(sendPerson);
                    await stream.WriteAsync(response, 0, response.Length);
                    Console.WriteLine("Ответ отправлен");
                }
            }
            catch (Exception) { }
        }
    }
}
