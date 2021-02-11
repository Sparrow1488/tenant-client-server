using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientChatVisual
{
    public partial class Form1 : Form
    {
        private static TcpClient client;
        private static StreamReader getMessagesStream;
        private static StreamWriter sendStream;
        private static List<string> CHAT;
        public Form1()
        {
            InitializeComponent();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            sendStream.WriteLine(textBox3.Text);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

        }
        private void RecieveMessages()
        {
            while (client?.Connected == true)
            {
                var response = getMessagesStream.ReadLine();
                if (response != null)
                {
                    CHAT.Add(response);
                }
                Task.Delay(200);
            }
            Console.WriteLine("смэрть");
        }

        private async void connectBtn_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            client.Connect("127.0.0.1", 8080);
            var btn = (Button)sender;
            btn.Enabled = false;
            getMessagesStream = new StreamReader(client?.GetStream());
            sendStream = new StreamWriter(client?.GetStream());
            await Task.Factory.StartNew(() => RecieveMessages());
        }
    }
}
