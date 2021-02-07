using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;

namespace Multi_Server_Test.ServerData.Server
{
    public class ServerMeta
    {
        public ServerMeta()
        {
            Console.WriteLine("Meta created");
        }
        public string usersPath = "Multi-server-users";
        public string newsPath = "Multi-server-news";

        public string reservePath = "reserve_data";
        public string reserveNewsCollection = "NEWS_COLLECTION.txt";
        public FirebaseClient firebaseClient = null;
        public IFirebaseConfig firebaseConfig = new FirebaseConfig()
        {
            AuthSecret = "6CScUkKUdSLgSDtq1QWtfY2NCPP57aa6ajBn7R4Y",
            BasePath = "https://client-server-testapp-default-rtdb.firebaseio.com/"
        };
    }
}
