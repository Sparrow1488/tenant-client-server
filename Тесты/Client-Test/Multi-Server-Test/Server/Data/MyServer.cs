﻿using FireSharp;
using FireSharp.Response;
using Multi_Server_Test.Blocks;
using Multi_Server_Test.Server.Packages;
using Multi_Server_Test.ServerData.Blocks;
using Multi_Server_Test.ServerData.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public ServerMeta Meta = null;
        public BlocksSection Blocks = null;

        public NewsCollection newsCollectionOutDB = null;

        public MyServer(string host, int port)
        {
            if(!string.IsNullOrWhiteSpace(host) &&
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
            foreach (var block in Blocks.ExistServerBlocks)
                Console.WriteLine(block.BlockAction + " is active");

            newsCollectionOutDB = await SynchronizeNewsCollection();
            foreach (var news in newsCollectionOutDB.Collection)
                Console.Write($"\t{news.Title}: {news.Description} <{news.DateTime.ToLongDateString()}> \n");

            Listener.Start();
            ShowReport("Server started!", ConsoleColor.Green);
        }
        public async Task AddUser(Person person)
        {
            if (!person.Equals(null))
            {
                Meta.firebaseClient = new FirebaseClient(Meta.firebaseConfig);
                await Meta.firebaseClient.SetAsync($"{Meta.usersPath}/{person.Login}", person);
            }
        }
        public async Task AddNewsCollection(NewsCollection collection)
        {
            if (collection != null)
            {
                Meta.firebaseClient = new FirebaseClient(Meta.firebaseConfig);
                var biba = await Meta.firebaseClient.SetAsync($"{Meta.newsPath}/{NewsCollection.Name}", collection);
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
        public void ReseveAndResponseToClient()
        {
            while (true)
            {
                ShowReport("Ожидаю запроса.......", ConsoleColor.White);
                var client = Listener.AcceptTcpClient();
                Thread handleClient = new Thread(new ParameterizedThreadStart(ResponseToClient));
                handleClient.Start(client);
            }
        }
        private void ResponseToClient(object tcpClient)
        {
            var validClient = tcpClient as TcpClient;
            if (validClient.Connected)
            {
                ShowReport("Client connect", ConsoleColor.Green);
                var clientStream = validClient.GetStream();
                byte[] buffer = new byte[1024];
                StringBuilder jsonPackage = new StringBuilder();
                do
                {
                    int bytes = clientStream.Read(buffer, 0, buffer.Length);
                    jsonPackage.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
                }
                while (clientStream.DataAvailable);
                var getPackage = JsonConvert.DeserializeObject<Package>(jsonPackage.ToString());
                Console.WriteLine("Получена мета: {0}, {1}", getPackage.SendingMeta.Address, getPackage.SendingMeta.Action);
                var clientObject = JsonConvert.SerializeObject(getPackage.SendingObject);

                ShowReport("Distribute request to handle in routing block...", ConsoleColor.Yellow);
                Router requestRoute = new Router(getPackage.SendingMeta.Action, clientObject, clientStream);
                requestRoute.CompleteRoute(Blocks.ExistServerBlocks);
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
                return newsCollectionResponse;
            }
            catch (NullReferenceException) 
            {
                //TODO: СДЕЛАТЬ РЕЗЕРВНОЕ КОПИРОВАНИЕ НОВОСТЕЙ
                return new NewsCollection(new List<News>() 
                { new News("RESERVER NEWS COLLECTION", 
                "DATABASE RETURN NULL NEWS COLLECTION, PLEASE, CHECK DATABASE AND RECOVERY DATA: server exception") 
                });
            }
        }

    }
}
