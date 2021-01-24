using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Classes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            excepyionLabel_TB.Visibility = Visibility.Collapsed;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            excepyionLabel_TB.Visibility = Visibility.Collapsed;
            try
            {
                send_Btn.IsEnabled = false;
                ClientData.client = new TcpClient();
                await ClientData.client.ConnectAsync("127.0.0.1", 8090);
                NetworkStream stream = ClientData.client.GetStream();
                byte[] sendData = Encoding.UTF8.GetBytes($"login: {login_TBox.Text}; password: {password_TBox.Password}");
                await stream.WriteAsync(sendData, 0, sendData.Length);

                string response = await GetData(ClientData.client);
                MessageBox.Show(response);

                stream.Close();
            }
            catch (SocketException)
            {
                excepyionLabel_TB.Visibility = Visibility.Visible;
                excepyionLabel_TB.Text = "Не удалось подключиться к серверу";
            }
            finally
            {
                send_Btn.IsEnabled = true;
            }
        }
        private async Task<string> GetData(TcpClient client)
        {
            StringBuilder response = new StringBuilder();
            byte[] getData = new byte[1024];
            await Task.Run(() =>
            {
                var getStream = client.GetStream();
                do
                {
                    int bytesSize = getStream.Read(getData, 0, getData.Length);
                    response.Append(Encoding.UTF8.GetString(getData, 0, bytesSize));
                }
                while (getStream.DataAvailable);
            });
            return response.ToString();
        }
    }
}
