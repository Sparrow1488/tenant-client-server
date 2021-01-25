using System;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Controls;

namespace Tenant_server_app.ServerClasses
{
    public abstract class ServerData
    {
        public static IPAddress IP = IPAddress.Parse("127.0.0.1");
        public static int PORT = 8090;
        public static TcpListener server = new TcpListener(IP, PORT);

        public static string usersPath = "Users";
        public static FirebaseClient serverClient = null;
        public static IFirebaseConfig firebaseConfig = new FirebaseConfig()
        {
            AuthSecret = "6CScUkKUdSLgSDtq1QWtfY2NCPP57aa6ajBn7R4Y",
            BasePath = "https://client-server-testapp-default-rtdb.firebaseio.com/"
        };

        public static async Task GetRequestsAsync(RichTextBox box)
        {
            box.AppendText("Wait for connecting...\n");
            //list.Items.Add("Wait for connecting...");
            TcpClient getClient = await server.AcceptTcpClientAsync();
            box.AppendText("Client connecting\n");
            //list.Items.Add("Client connecting");

            NetworkStream stream = getClient.GetStream();
            byte[] getData = new byte[2048];
            StringBuilder response = new StringBuilder();

            if (stream.CanRead)
            {
                do
                {
                    int bytes = await stream.ReadAsync(getData, 0, getData.Length);
                    response.Append(Encoding.UTF8.GetString(getData, 0, bytes));
                }
                while (stream.DataAvailable);
                box.AppendText("Get data: " + response + "\n");
                //list.Items.Add("Get data: " + response);
            }
            else
            {
                box.AppendText("Get stream can not read\n");
                //list.Items.Add("Get stream can not read");
            }

            byte[] sendData = Encoding.UTF8.GetBytes("Если Вы получили это сообщение, значит сервер обработал Ваш запрос");
            await stream.WriteAsync(sendData, 0, sendData.Length);
            box. AppendText("Reply sent\n");
            stream.Close();
        }
    }
}
