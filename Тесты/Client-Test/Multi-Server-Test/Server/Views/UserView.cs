using Multi_Server_Test.Server.Models;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Multi_Server_Test.Server.Views
{
    public class UserView : ViewModule
    {
        private ServerModulEvents serverEvents = new ServerModulEvents();
        public UserView(byte[] responseData, NetworkStream writeStream) : base(responseData, writeStream) { viewName = "UserView"; }

        public override async Task ExecuteModuleProcessing(string additionalMessage)
        {
            if (WriteStream.CanWrite)
            {
                serverEvents.BlockReport(this, "Данные отправлены", ConsoleColor.Green);
                await WriteStream.WriteAsync(ResponseData, 0, ResponseData.Length);
                WriteStream.Close();
            }
            else
            {
                serverEvents.BlockReport(this, "Ошибка записи потока: не поддерживается запись", ConsoleColor.Yellow);
            }
        }
    }
}
