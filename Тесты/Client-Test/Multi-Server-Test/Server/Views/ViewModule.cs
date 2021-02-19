using System.Net.Sockets;
using System.Threading.Tasks;

namespace Multi_Server_Test.Server.Models
{
    public abstract class ViewModule
    {
        protected string viewName = "view_module";
        public string ViewName 
        { 
            get { return viewName; } 
        } 
        public NetworkStream WriteStream { get; }
        public byte[] ResponseData { get; }
        public ViewModule(byte[] responseData, NetworkStream writeStream) //первый агрумент byte[] с уже закодированным объектом
        {
            ResponseData = responseData;
            WriteStream = writeStream;
        }
        public abstract Task ExecuteModuleProcessing(string additionalMessage);
    }
}
