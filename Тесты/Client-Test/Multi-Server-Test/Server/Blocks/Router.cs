using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Multi_Server_Test.ServerData.Blocks
{
    public class Router
    {
        public string RouteAction { get; }
        public string JsonClinetObject { get; }
        public NetworkStream ClientResponseStream { get; }
        public Router(string action, string clientObject, NetworkStream stream)
        {
            RouteAction = action;
            JsonClinetObject = clientObject;
            ClientResponseStream = stream;
        }

        public void CompleteRoute(List<ServerBlock> listServerBlocks)
        {
            foreach (var block in listServerBlocks)
            {
                if (RouteAction.Equals(block.BlockAction))
                {
                    block.CompleteAction(JsonClinetObject, ClientResponseStream);
                    break;
                }
            }
        }

    }
}
