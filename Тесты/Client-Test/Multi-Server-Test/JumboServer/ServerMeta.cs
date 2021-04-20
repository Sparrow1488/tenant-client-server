using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace JumboServer.Meta
{
    public class ServerMeta
    {
        #region Constructor
        public ServerMeta()
        {
            Console.WriteLine("Meta creating...");
            CreateSqlConnection();
            Console.WriteLine("Meta created");
        }
        #endregion

        #region Props
        public string reservePath = "reserve_data";
        public string reserveNewsCollectionTxt = "NEWS_COLLECTION.txt";
        public string reserveLettersTxt = "LETTERS_ALL.txt";
        public string reserveUsersTxt = "LETTERS_ALL.txt";
        public static Encoding Encoding = Encoding.UTF32;
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DOM\Desktop\ИЛЬЯ\HTML\C#\tenant-client-server\Тесты\Client-Test\Multi-Server-Test\JumboDataBase.mdf;Integrated Security=True";
        
        public string PublicRSAKey { get; set; }
        public string PrivateRSAKey { get; set; }
        public string AesKey { get; set; }
        #endregion

        #region Methods
        private void CreateSqlConnection()
        {
            using (var testConnection = new SqlConnection(connectionString))
            {
                testConnection.Open();
                if (testConnection.State == ConnectionState.Open)
                    Console.WriteLine("Успешное подключение к СУБД");
                else
                    Console.WriteLine("ОШИБКА ПОДКЛЮЧЕНИЯ К СУБД");
                testConnection.Close();
            }
        }
        #endregion
    }
}
