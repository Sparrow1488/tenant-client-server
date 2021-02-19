﻿using FireSharp;
using FireSharp.Response;
using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Blocks.Auth;
using Multi_Server_Test.Server.Controllers;
using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Blocks.Auth;
using Multi_Server_Test.ServerData.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multi_Server_Test.ServerData
{
    public class MyServer
    {
        public string HOST = null;
        public int PORT = 0;
        public string serverName { get; }

        public TcpListener Listener = null;
        public static ServerMeta Meta = null;
        public BlocksSection Blocks = null;

        public static NewsCollection newsCollectionOutDB = null;
        public MyServer(string host, int port)
        {
            if (!string.IsNullOrWhiteSpace(host) &&
                port > 10)
            {
                HOST = host;
                PORT = port;
                serverName = "MyServer";

                Listener = new TcpListener(IPAddress.Parse(HOST), PORT);
                Meta = new ServerMeta();
                Blocks = new BlocksSection(this); //передаем объект сервера для возможности использовать его методы
            }
            else
            {
                throw new ArgumentException("Вы передали некорректные значения");
            }
        }
        public async Task Start()
        {
            foreach (var block in Blocks.ExistServerModels)
                Console.WriteLine(block.Action + " is active");

            newsCollectionOutDB = await SynchronizeNewsCollection();
            foreach (var news in newsCollectionOutDB.Collection)
                Console.WriteLine(news + "\n");

            Listener.Start();
            ShowReport("Server started!", ConsoleColor.Green);
        }
        public async Task AddUser(Person person)
        {
            if (!person.Equals(null))
            {
                Meta.firebaseClient = new FirebaseClient(Meta.firebaseConfig); //TODO: клиент зачем то постоянно создается
                await Meta.firebaseClient.SetAsync($"{Meta.usersPath}/{person.Login}", person);
                ShowReport("Person was loaded on server", ConsoleColor.Green);
            }
        }
        public void AddNewsOnServer(News news) //TODO: сделать отправку в БД (через определенное время / после 5 новых новостей)
        {
            if (newsCollectionOutDB.Collection != null)
            {
                newsCollectionOutDB.Collection.Add(news);
                ShowReport("Новость успешно добавлена", ConsoleColor.Yellow);
                //Meta.firebaseClient = new FirebaseClient(Meta.firebaseConfig);
                //await Meta.firebaseClient.UpdateAsync($"{Meta.newsPath}/{NewsCollection.Name}", newCollection);
                //ShowReport("Новость добавлена в базу. Выполняется синхронизация...", ConsoleColor.Yellow);
                //newsCollectionOutDB = await SynchronizeNewsCollection();
                //ShowReport("Новость успешно загружена.", ConsoleColor.Green);
            }
        }
        public async Task<Person> GetUser(Person person)
        {
            FirebaseResponse respose;
            try
            {
                Meta.firebaseClient = new FirebaseClient(Meta.firebaseConfig);
                respose = await Meta.firebaseClient.GetAsync($"{Meta.usersPath}/{person.Login}");
            }
            catch (NullReferenceException) { return null; }

            var user = respose.ResultAs<Person>();
            return user;
        }
        public async Task<NewsCollection> GetNewsCollection()
        {
            FirebaseResponse respose;
            try
            {
                Meta.firebaseClient = new FirebaseClient(Meta.firebaseConfig);
                respose = await Meta.firebaseClient.GetAsync($"{Meta.newsPath}/{NewsCollection.Name}");
            }
            catch (NullReferenceException) { return null; }

            var getNewsCollection = respose.ResultAs<NewsCollection>();
            return getNewsCollection;
        }
        public void ServeAndResponseToClient()
        {
            while (true)
            {
                ShowReport("Ожидаю запроса.......", ConsoleColor.White);
                var client = Listener.AcceptTcpClient();
                //Thread handleClient = new Thread(new ParameterizedThreadStart(ResponseToClient));
                //handleClient.Start(client);
                Task.Factory.StartNew(() => ResponseToClient(client));
            }
        }
        private void ResponseToClient(object tcpClient)
        {
            var validClient = tcpClient as TcpClient;
            if (validClient.Connected)
            {
                ShowReport("Client connect", ConsoleColor.Green);

                var clientStream = validClient.GetStream();
                string jsonPackage = GetDataFromStream(clientStream);
                if(jsonPackage == null)
                {
                    ShowReport("Ошибка: поток не поддерживает чтение", ConsoleColor.Red);
                    return;
                }

                if (!string.IsNullOrWhiteSpace(jsonPackage.ToString()))
                {
                    var getPackage = JsonConvert.DeserializeObject<Package>(jsonPackage.ToString());
                    Console.WriteLine(getPackage.SendingMeta);
                    var clientObject = JsonConvert.SerializeObject(getPackage.SendingObject);

                    ShowReport("Distribute request to handle in main routing controller...", ConsoleColor.Yellow);

                    var userModels = new List<Model>()
                    { new AuthorizationModel("auth") };

                    var newsModels = new List<Model>()
                    { new GetNewsCollectionModel("get") };

                    List<Controller> newControllers = new List<Controller>()
                    {
                        new UserController("User", userModels),
                        new NewsController("News", newsModels)
                    };

                    MainRouter router = new MainRouter(newControllers);
                    router.ExecuteRouting(getPackage, validClient);
                }
                else
                {
                    ShowReport("Пакет данных не может быть получен!", ConsoleColor.Red);
                }

            }
            else
            {
                ShowReport("Client not connected", ConsoleColor.Red);
            }
            
        }
        
        private void ShowReport(string report, ConsoleColor color)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(serverName);
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("> ");

            Console.ForegroundColor = color;
            Console.WriteLine(report);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private async Task<NewsCollection> SynchronizeNewsCollection()
        {
            try
            {
                var newsCollectionResponse = await GetNewsCollection();
                ShowReport("News was loaded successful", ConsoleColor.Green);
                if (File.Exists($"{Meta.reservePath}/{Meta.reserveNewsCollection}"))
                {
                    using (var writer = new StreamWriter($"{Meta.reservePath}/{Meta.reserveNewsCollection}"))
                    {
                        var dataJson = JsonConvert.SerializeObject(newsCollectionResponse);
                        await writer.WriteAsync(dataJson);
                        ShowReport("News saved", ConsoleColor.Green);
                    }
                }
                else
                {
                    ShowReport("Резервная папка не найдена! Создаю новую директорию...", ConsoleColor.Red);
                    Directory.CreateDirectory(Meta.reservePath);
                    using (var stream = File.CreateText($"{Meta.reservePath}/{Meta.reserveNewsCollection}"))
                    {
                        var dataJson = JsonConvert.SerializeObject(newsCollectionResponse);
                        await stream.WriteAsync(dataJson);
                        ShowReport("News saved", ConsoleColor.Green);
                    }
                }
                return newsCollectionResponse;
            }
            catch (NullReferenceException) 
            {
                ShowReport("News cannot be retrieved from the database!", ConsoleColor.Red);
                ShowReport($"Trying execute load from ./{Meta.reservePath}/...", ConsoleColor.Yellow);
                using (var reader = new StreamReader($"{Meta.reservePath}/{Meta.reserveNewsCollection}"))
                {
                    var reserveCollectionJson = reader.ReadToEnd();
                    var reserveCollection = JsonConvert.DeserializeObject<NewsCollection>(reserveCollectionJson);
                    return reserveCollection;
                }
            }
        }
        #region Вторичные методы
        private string GetDataFromStream(NetworkStream clientStream)
        {
            if (clientStream.CanRead)
            {
                byte[] buffer = new byte[1024];
                StringBuilder jsonPackage = new StringBuilder();
                do
                {
                    int bytes = clientStream.Read(buffer, 0, buffer.Length);
                    jsonPackage.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
                }
                while (clientStream.DataAvailable);
                return jsonPackage.ToString();
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
