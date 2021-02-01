using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Multi_Server_Test.Blocks;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Server_Test.ServerData
{
    public class ServerMethods
    {
        public string HOST = "127.0.0.1";
        public int PORT;
        public TcpListener serverListener = null;
        public ServerMethods(string host, int port)
        {
            if(!string.IsNullOrWhiteSpace(host) &&
                port > 10)
            {
                HOST = host;
                PORT = port;
            }
            else
            {
                throw new ArgumentException("Вы передали некорректные значения");
            }
        }

        private static string usersPath = "Multi-server-users";
        private static FirebaseClient serverClient = null;
        private static IFirebaseConfig firebaseConfig = new FirebaseConfig()
        {
            AuthSecret = "6CScUkKUdSLgSDtq1QWtfY2NCPP57aa6ajBn7R4Y",
            BasePath = "https://client-server-testapp-default-rtdb.firebaseio.com/"
        };

        public static async Task AddInDb(Person person)
        {
            if (!person.Equals(null))
            {
                await Task.Run(() =>
                {
                    serverClient.SetAsync($"{usersPath}/{person.Login}", person);
                });
            }
        }
        public static async Task<Person> GetUserOutDB(Person person)
        {
            try
            {
                serverClient = new FirebaseClient(firebaseConfig);
                var respose = await serverClient.GetAsync($"{usersPath}/{person.Login}");

                if (respose.Equals(null))
                {
                    throw new NullReferenceException("Данного пользователя не существует");
                }

                var user = respose.ResultAs<Person>();
                return user;
            }
            catch (NullReferenceException) { return null; }
        }
    }
}
