using Multi_Server_Test.Server.Blocks.Auth;
using Multi_Server_Test.Server.Blocks.LetterBlock;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Blocks.Auth;
using System;
using System.Collections.Generic;

namespace Multi_Server_Test.ServerData.Server
{
    public class BlocksSection //TODO: а он точно нужен?
    {
        public List<ViewModule> ExistServerBlocks = new List<ViewModule>();
        public BlocksSection(MyServer usageServer)
        {
            Create(usageServer);
        }
        public void Create(MyServer usageServer)
        {
            ExistServerBlocks.Add(new AuthorizationModule("auth", usageServer));
            ExistServerBlocks.Add(new GetNewsCollectionModule("news", usageServer));
            ExistServerBlocks.Add(new LetterModule("letter", usageServer));

            Console.WriteLine("Blocks created");
        }
    }
}
