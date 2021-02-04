using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Multi_Server_Test.ServerData.Blocks
{
    public abstract class ServerBlock
    {
        public MyServer requestServer;
        public string BlockAction { get; }
        public ServerBlock(string blockAction, MyServer server)
        {
            if(blockAction.Length >= 4)
            {
                BlockAction = blockAction;
                requestServer = server;
            }
            else
                throw new ArgumentException("Название блока должно быть больше 4-х символов");
        }
        public abstract void CompleteAction(string clientJson, NetworkStream stream);
    }
}
