using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Models
{
    public abstract class Model
    {
        public string[] processCommands { get; }
        public Model()
        {

        }
        public abstract string ExecuteModelProcessing(string[] instructions);
    }
}
