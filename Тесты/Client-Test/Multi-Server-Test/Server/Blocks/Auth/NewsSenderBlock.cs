using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.ServerData.Blocks;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;

namespace Multi_Server_Test.Server.Blocks.Auth
{
    public class NewsSenderBlock : ServerBlock
    {
        public NewsSenderBlock(string blockAction, MyServer server) : base(blockAction, server) { }

        public override async void CompleteAction(string clientJson, NetworkStream stream)
        {
            var allNews = await RequestServer.GetNews();
            //foreach (var news in )
            //{
            //    Console.WriteLine(news.Title);
            //}
            Console.WriteLine(BlockAction + " до связи...");
        }
    }
}
