using System;
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
        private static TcpClient clientSend;
        private static NetworkStream getMessagesStream;
        private static NetworkStream sendStream;
        public Form1()
        {
            InitializeComponent();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            sendStream = clientSend.GetStream();
            byte[] sendMessage = Encoding.UTF8.GetBytes(textBox3.Text);
            sendStream.Write(sendMessage, 0, sendMessage.Length);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

        }
        private void RecieveMessages()
        {
            while (true)
            {
                getMessagesStream = clientSend.GetStream();
                var response = new StringBuilder();
                var getData = new byte[256];
                do
                {
                    int bytes = getMessagesStream.Read(getData, 0, getData.Length);
                    response.Append(Encoding.UTF8.GetString(getData, 0, bytes));
                }
                while (getMessagesStream.DataAvailable);
                textBox1.Text += response;
                textBox1.Text += Environment.NewLine;
                getMessagesStream.Close();
            }
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            clientSend = new TcpClient();
            clientSend.Connect("127.0.0.1", 8080);
            var btn = (Button)sender;
            btn.Enabled = false;
            //Thread getMessagesThread = new Thread(new ThreadStart(RecieveMessages));
            //getMessagesThread.Start();
        }
    }
}
