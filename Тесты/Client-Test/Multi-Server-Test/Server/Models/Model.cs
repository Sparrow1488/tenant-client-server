using System;

namespace Multi_Server_Test.ServerData.Blocks
{
    public abstract class Model
    {
        public string Action { get; }
        public bool OnlyAdmin { get; }
        public Model(string modelAction, bool forOnlyAdmin)
        {
            Action = modelAction;
            OnlyAdmin = forOnlyAdmin;
        }

        public abstract byte[] CompleteAction(object reqObject);
    }
}
