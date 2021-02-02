using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

        public async Task CompleteRouteAsync()
        {
            foreach (var block in ServerBlock.ExistsServerBlocks)
            {
                if (RouteAction.Equals(block.BlockAction))
                {
                    await block.CompleteAction(JsonClinetObject, ClientResponseStream);
                    break;
                }
            }
        }

    }
}
