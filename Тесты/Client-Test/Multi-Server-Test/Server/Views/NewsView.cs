using Multi_Server_Test.Server.Packages;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Multi_Server_Test.Server.Models
{
    public class NewsView : ViewModule
    {
        ServerModulEvents serverEvents = new ServerModulEvents();
        public NewsView(byte[] responseData, NetworkStream writeStream) : base(responseData, writeStream) { viewName = "NewsView"; }

        public override async Task ExecuteModuleProcessing(string additionalMessage)
        {
            if (WriteStream.CanWrite)
            {
                serverEvents.BlockReport(this, "Данные отправлены", ConsoleColor.Green);
                await WriteStream.WriteAsync(ResponseData, 0, ResponseData.Length);
                WriteStream.Close();
            }
            else
                serverEvents.BlockReport(this, "Ошибка записи в поток: не поддерживается запись", ConsoleColor.Yellow);
        }
    }
}
