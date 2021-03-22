using System;

namespace JumboServer.Models
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
