using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

namespace Multi_Server_Test.Server.Blocks.Auth
{
    public class GetNewsCollectionModule : ViewModule
    {
        public GetNewsCollectionModule(string blockAction, MyServer server) : base(blockAction, server) { }

        public override async void CompleteAction(string clientJson, NetworkStream stream, string[] commands)
        {
            var newsCollectionToResponse = RequestServer.newsCollectionOutDB;

            try
            {
                var jsonNewsCollection = JsonConvert.SerializeObject(newsCollectionToResponse);
                var response = Encoding.UTF8.GetBytes(jsonNewsCollection);
                await stream.WriteAsync(response, 0, response.Length);

                BlockReport("Коллекция новостей отправлена", ConsoleColor.Green);
            }
            catch (Exception) { }
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
