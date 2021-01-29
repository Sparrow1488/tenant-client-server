﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using WpfApp1.Blocks;
using WpfApp1.Classes;
using WpfApp1.Classes.Client.Requests;
using WpfApp1.Server.Packages;
using WpfApp1.Server.UserInfo;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServerConfig config = new ServerConfig(ServerConfig.HOST, ServerConfig.PORT);

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            exceptionLabel_TB.Visibility = Visibility.Collapsed;
        }

        private async void sendRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            exceptionLabel_TB.Visibility = Visibility.Collapsed;
            send_Btn.IsEnabled = false;

            var reqPerson = new Person(login_TBox.Text, password_TBox.Password, 67);
            var reqTest = new Test("TEST");
            var reqMeta = new SendMeta("127.0.0.1", "auth");
            var package = new PersonRequest(reqTest, reqMeta);
            
            try
            {
                MyServer server = new MyServer(config);
                server.CreateClient();
                await server.SendRequestAsync(package);
                //string response = await server.GetAsync();
                //MessageBox.Show(response, "Server response");
            }
            catch (SocketException)
            {
                exceptionLabel_TB.Visibility = Visibility.Visible;
                exceptionLabel_TB.Text = "Не удалось подключиться к серверу";
            }
            finally
            {
                send_Btn.IsEnabled = true;
            }
        }
        private Package CreateRequestPackage(RequestObject request, SendMeta meta)
        {
            return new PersonRequest(request, meta);
        }

    }
}
