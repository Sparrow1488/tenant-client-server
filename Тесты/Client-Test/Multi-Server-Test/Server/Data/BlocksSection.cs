using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Blocks.Auth;
using System;
using System.Collections.Generic;

namespace Multi_Server_Test.ServerData.Server
{
    public class BlocksSection
    {
        public List<ServerBlock> ExistServerBlocks = new List<ServerBlock>();
        public BlocksSection(MyServer usageServer)
        {
            Create(usageServer);
        }
        public void Create(MyServer usageServer)
        {
            ExistServerBlocks.Add(new AuthorizationBlock("auth", usageServer));

            Console.WriteLine("Blocks created");
        }
    }
}
