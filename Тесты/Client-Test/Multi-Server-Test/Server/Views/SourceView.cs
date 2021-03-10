using Multi_Server_Test.Server.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Multi_Server_Test.Server.Views
{
    public class SourceView : ViewModule
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        public SourceView(byte[] responseData, NetworkStream writeStream) : base(responseData, writeStream)
        {
            viewName = "SourceView";
        }

        public override void ExecuteModuleProcessing(string additionalMessage)
        {
            if (WriteStream.CanWrite)
            {
                serverEvents.BlockReport(this, "Данные отправлены", ConsoleColor.Green);
                WriteStream.Write(ResponseData, 0, ResponseData.Length);
                WriteStream.Close();
            }
            else
                serverEvents.BlockReport(this, "Ошибка записи в поток: не поддерживается запись", ConsoleColor.Yellow);
        }
    }
}
