using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Server_Test.Server.Blocks.LetterBlock
{
    public class LetterGetterBlock : ServerBlock
    {
        public LetterGetterBlock(string blockAction, MyServer server) : base(blockAction, server) { }

        public override async void CompleteAction(string clientJson, NetworkStream stream)
        {
            try
            { 
                var getLetter = JsonConvert.DeserializeObject<Letter>(clientJson);
                BlockReport("Письмо успешно получено", ConsoleColor.Green);
                Console.WriteLine(getLetter); //TODO: сделать сортер по типу новости (предложение, жалоба, вопрос)
                var data = Encoding.UTF8.GetBytes("Письмо получено");
                await stream.WriteAsync(data, 0, data.Length);
            }
            catch (Exception) { }
        } 
        private void SaveRecivedLetter()
        {

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
