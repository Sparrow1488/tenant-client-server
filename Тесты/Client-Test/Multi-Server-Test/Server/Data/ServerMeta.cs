using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Multi_Server_Test.ServerData.Server
{
    public class ServerMeta
    {
        public SqlConnection sqlConnection = null;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DOM\Desktop\ИЛЬЯ\HTML\C#\proj\tenant-client-server\Тесты\Client-Test\Multi-Server-Test\JumboDataBase.mdf;Integrated Security=True";
        public ServerMeta()
        {
            Console.WriteLine("Meta creating...");
            CreateSqlConnection();
            Console.WriteLine("Meta created");
        }
        
        private void CreateSqlConnection()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            if(sqlConnection.State == ConnectionState.Open)
                Console.WriteLine("Успешное подключение к СУБД");
            else
                Console.WriteLine("ОШИБКА ПОДКЛЮЧЕНИЯ К СУБД");
        }
        public string reservePath = "reserve_data";
        public string reserveNewsCollectionTxt = "NEWS_COLLECTION.txt";
        public string reserveLettersTxt = "LETTERS_ALL.txt";
        public string reserveUsersTxt = "LETTERS_ALL.txt";
        public FirebaseClient firebaseClient = null;
        public IFirebaseConfig firebaseConfig = new FirebaseConfig()
        {
            AuthSecret = "6CScUkKUdSLgSDtq1QWtfY2NCPP57aa6ajBn7R4Y",
            BasePath = "https://client-server-testapp-default-rtdb.firebaseio.com/"
        };
    }
}
