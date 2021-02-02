using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Multi_Server_Test.ServerData.Blocks
{
    public abstract class ServerBlock
    {
        public static List<string> ExistsActions = new List<string>();
        public static List<ServerBlock> ExistsServerBlocks = new List<ServerBlock>();
        public string BlockAction { get; }
        public ServerBlock(string blockAction)
        {
            if(blockAction.Length >= 4)
            {
                BlockAction = blockAction;
                ExistsActions.Add(BlockAction);
                ExistsServerBlocks.Add(this);
            }
            else
            {
                throw new ArgumentException("Название блока должно быть больше 4-х символов");
            }
        }
        public abstract Task CompleteAction(string clientJson, NetworkStream stream);
    }
}
