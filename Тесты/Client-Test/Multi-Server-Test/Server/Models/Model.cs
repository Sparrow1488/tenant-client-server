using System;

namespace Multi_Server_Test.ServerData.Blocks
{
    public abstract class Model
    {
        public string Action { get; }
        public Model(string modelAction)
        {
            Action = modelAction;
        }

        public abstract byte[] CompleteAction(object reqObject);
    }
}
