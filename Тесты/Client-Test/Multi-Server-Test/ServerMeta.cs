using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace JumboServer.Meta
{
    public class ServerMeta
    {
        #region Props
        public string reservePath = "reserve_data";
        public string reserveNewsCollectionTxt = "NEWS_COLLECTION.txt";
        public string reserveLettersTxt = "LETTERS_ALL.txt";
        public string reserveUsersTxt = "LETTERS_ALL.txt";
        public static Encoding Encoding = Encoding.UTF32;
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DOM\Desktop\ИЛЬЯ\HTML\C#\tenant-client-server\Тесты\Client-Test\Multi-Server-Test\JumboDataBase.mdf;Integrated Security=True";
        #endregion

        #region Methods
        public void TestSqlConnection()
        {
            using (var testConnection = new SqlConnection(connectionString))
            {
                testConnection.Open();
                if (testConnection.State == ConnectionState.Open)
                    Console.WriteLine("Успешное подключение к СУБД");
                else
                    throw new ArgumentNullException("ОШИБКА ПОДКЛЮЧЕНИЯ К СУБД");
                testConnection.Close();
            }
        }
        #endregion
    }
}
