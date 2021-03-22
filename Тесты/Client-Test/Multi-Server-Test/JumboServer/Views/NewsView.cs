using JumboServer.Functions;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace JumboServer.Views
{
    public class NewsView : ViewModule
    {
        ServerReportsModule serverEvents = new ServerReportsModule();
        public NewsView(byte[] responseData, NetworkStream writeStream) : base(responseData, writeStream) { viewName = "NewsView"; }

        public override void ExecuteModuleProcessing()
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
