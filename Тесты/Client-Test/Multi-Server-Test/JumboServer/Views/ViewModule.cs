using JumboServer.Functions;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace JumboServer.Views
{
    public abstract class ViewModule
    {
        #region Constructor
        public ViewModule(ref TcpClient client)
        {
            WriteStream = client?.GetStream();
        }
        #endregion

        #region Props
        public string ViewName { get; protected set; } = "view_module";
        public NetworkStream WriteStream { get; }
        public byte[] ResponseData { get; set; }
        #endregion

        #region AdditionalInstances
        private ServerReportsModule serverEvents = new ServerReportsModule();
        #endregion

        #region Methods
        public abstract void ExecuteModuleProcessing(ref byte[] responseData);
        public void SendResponse(ref byte[] responseData)
        {
            ResponseData = responseData;
            if (WriteStream.CanWrite)
            {
                serverEvents.BlockReport(this, "Данные отправлены", ConsoleColor.Green);
                try
                {
                    WriteStream.Write(ResponseData, 0, ResponseData.Length);
                    WriteStream.Close();
                }
                catch { serverEvents.BlockReport(this, "Возникла ошибка в процессе записи в поток", ConsoleColor.Yellow); }
            }
            else
                serverEvents.BlockReport(this, "Ошибка записи в поток: не поддерживается запись", ConsoleColor.Yellow);
        }
        #endregion

    }
}
