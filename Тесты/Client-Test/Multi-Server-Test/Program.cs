using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Packages;
using Newtonsoft.Json;
using Multi_Server_Test.ServerData;

namespace Multi_Server_Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var server = new ServerData.Server("127.0.0.1", 8090);
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8090);
            server.Start();

            while (true)
            {
                Console.WriteLine("Ожидаю запроса.......");
                var client = server.AcceptTcpClient();
                Console.WriteLine("Client connect");

                var stream = client.GetStream();
                byte[] buffer = new byte[1024];
                StringBuilder jsonPackage = new StringBuilder();
                do
                {
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    jsonPackage.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
                }
                while (stream.DataAvailable);
                var getPackage = JsonConvert.DeserializeObject<Package>(jsonPackage.ToString());
                Console.WriteLine("Получена мета: {0}, {1}", getPackage.SendingMeta.Address, getPackage.SendingMeta.Action);

                ////TODO: сделать маршрутизатор запроса
                if (getPackage.SendingMeta.Action.Equals("auth"))
                {
                    try
                    {
                        var jsonPerson = JsonConvert.SerializeObject(getPackage.SendingObject);
                        var getPerson = JsonConvert.DeserializeObject<Person>(jsonPerson);

                        var wasPerson = await ServerMethods.GetUserOutDB(getPerson);
                        if (wasPerson.Equals(null))
                        {
                            //await ServerMethods.AddInDb(getPerson);
                            //Console.WriteLine("Пользователь создан");

                            //var response = Encoding.UTF8.GetBytes("");
                            //await stream.WriteAsync(response, 0, response.Length);
                            //Console.WriteLine("Ответ отправлен");
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
    }
    
}

