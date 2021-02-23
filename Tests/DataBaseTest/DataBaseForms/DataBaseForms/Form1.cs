using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseForms
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DOM\Desktop\ИЛЬЯ\HTML\C#\tenant-client-server\Tests\DataBaseTest\DataBaseForms\DataBaseForms\Database1.mdf;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open)
                MessageBox.Show("Подключение к базе данных: успешно", "УСПЕХ");
            else
                MessageBox.Show("Подключение к базе данных: успешно", "ОШИБКА");
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            string phoneTbRes = phoneTB.Text;
            bool intertPhoneIsInt = int.TryParse(phoneTbRes, out int res);
            //<<<---СПОСОБ ПЕРВЫЙ (В ОДНОЙ СТРОКЕ)--->>>
            //if (intertPhoneIsInt)
            //{
            //    var command = new SqlCommand($"INSERT INTO [Users] (Login, Password, Name, Phone) " +
            //                                 $"VALUES (N'{loginTB.Text}', " +
            //                                         $"N'{passwordTB.Text}', " +
            //                                         $"N'{nameTB.Text}', " +
            //                                         $"N'{phoneTB.Text}')", sqlConnection);
            //    int success = command.ExecuteNonQuery(); //выполняет запрос и возвращает кол-во успешно выполненных строк (запросов)
            //    MessageBox.Show(success.ToString(), "УСПЕШНО");
            //}
            //else
            //    MessageBox.Show("Поле PHONE не типа int", "ОШИБКА");

            //<<<---СПОСОБ ВТОРОЙ (ПО КЛЮЧУ)--->>>
            if (intertPhoneIsInt)
            {
                var command = new SqlCommand("INSERT INTO [Users] (Login, Password, Name, Phone) VALUES (@Log, @Password, @Name, @Phone)", sqlConnection);
                command.Parameters.AddWithValue("Log", loginTB.Text); //по ключу, который мы обозначили выше, добавляем нужное значение
                command.Parameters.AddWithValue("Password", passwordTB.Text);
                command.Parameters.AddWithValue("Name", nameTB.Text);
                command.Parameters.AddWithValue("Phone", phoneTB.Text);
                int success = command.ExecuteNonQuery(); //выполняет запрос и возвращает кол-во успешно выполненных строк (запросов)
                MessageBox.Show(success.ToString(), "УСПЕШНО");
            }
            else
                MessageBox.Show("Поле PHONE не типа int", "ОШИБКА");
        }
    }
}
