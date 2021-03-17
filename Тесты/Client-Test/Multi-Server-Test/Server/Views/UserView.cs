using Multi_Server_Test.Server.Models;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Multi_Server_Test.Server.Views
{
    public class UserView : ViewModule
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        public UserView(byte[] responseData, NetworkStream writeStream) : base(responseData, writeStream) { viewName = "UserView"; }

        public override void ExecuteModuleProcessing(string additionalMessage)
        {
            if (WriteStream.CanWrite)
            {
                serverEvents.BlockReport(this, "Данные отправлены", ConsoleColor.Green);
                WriteStream.Write(ResponseData, 0, ResponseData.Length); //TODO: проверка на null
                WriteStream.Close();
            }
            else
            {
                serverEvents.BlockReport(this, "Ошибка записи в поток: не поддерживается запись", ConsoleColor.Yellow);
            }
        }
    }
}
