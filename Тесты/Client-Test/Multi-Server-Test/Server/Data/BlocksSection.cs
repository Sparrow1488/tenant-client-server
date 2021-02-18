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
        public List<Model> ExistServerModels = new List<Model>();
        public BlocksSection(MyServer usageServer)
        {
            Create(usageServer);
        }
        public void Create(MyServer usageServer)
        {
            //ExistServerModels.Add(new AuthorizationModel("auth"));
            //ExistServerModels.Add(new GetNewsCollectionModel("news/get"));
            //ExistServerModels.Add(new GetLetterModel("letter/get"));

            //Console.WriteLine("Blocks created");
        }
    }
}
