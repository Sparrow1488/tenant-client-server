using Multi_Server_Test.Server.Blocks;
using Multi_Server_Test.Server.Packages;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Multi_Server_Test.ServerData.Blocks
{
    public abstract class Model
    {
        //public NetworkStream ResponseStream;
        public string Action { get; }
        public Model(string modelAction)
        {
            Action = modelAction;
        }

        public abstract Task<byte[]> CompleteAction(object reqObject);

    }
}
