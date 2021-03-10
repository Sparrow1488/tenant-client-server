using Multi_Server_Test.ServerData.Blocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Models.SourceBlock
{
    public class AddSourceModel : Model
    {
        public AddSourceModel(string modelAction) : base(modelAction) { }

        public override byte[] CompleteAction(object reqObject)
        {
            throw new NotImplementedException();
        }
    }
}
