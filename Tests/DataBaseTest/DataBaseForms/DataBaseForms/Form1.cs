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
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dom\Desktop\Репозитории\tenant-client-server\Tests\DataBaseTest\DataBaseForms\DataBaseForms\Database1.mdf;Integrated Security=True";
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
    }
}
