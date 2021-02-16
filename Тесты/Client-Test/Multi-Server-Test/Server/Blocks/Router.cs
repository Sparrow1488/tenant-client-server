using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Multi_Server_Test.ServerData.Blocks
{
    public class Router
    {
        public string RouteAction { get; }
        //public string AdditionalRouteAction { get; } //TODO: подумать над обработкой дополнительных прилетевших параметров 
        public string JsonClinetObject { get; }
        public NetworkStream ClientResponseStream { get; }
        public Router(string action, string clientJsonObject, NetworkStream stream)
        {
            //string[] actions = action.Split('/');
            RouteAction = action;
            //AdditionalRouteAction = actions[1];
            JsonClinetObject = clientJsonObject;
            ClientResponseStream = stream;
        }

        public void CompleteRoute(List<ServerBlock> listServerBlocks)
        {
            for (int i = 0; i < listServerBlocks.Count; i++)
            {
                var block = listServerBlocks[i];
                if (RouteAction.Equals(block.BlockAction))
                {
                    block.CompleteAction(JsonClinetObject, ClientResponseStream);
                    break;
                }
                if (i == listServerBlocks.Count - 1)
                {
                    Console.WriteLine("Под данный запрос не найдено функций");
                    break;
                }
            }
        }

    }
}
