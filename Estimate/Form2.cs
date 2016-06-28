using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Estimate
{
    public partial class Form2 : Form
    {
        string connectionString = "Database=estimate;Data Source=localhost;User Id=root;Password=";
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                FillMethod();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка получения данных: " + Environment.NewLine + ex.ToString()); }
        }
        private void FillMethod()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT `AIS_Name` FROM AIS";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.SelectCommand = command;
            adapter.Fill(ds, "AIS");

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 1354;

            connection.Close();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);

                if (textBoxName.Text != "")
                {
                    connection.Open();
                    string query = "INSERT INTO AIS (AIS_Name) VAlUES ('" + textBoxName.Text + "')";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                else MessageBox.Show("Для добавления новой АИС заполните все поля!");
                FillMethod();
                textBoxName.Text = "";
            }
            catch (Exception ex) { MessageBox.Show("Ошибка получения данных: " + Environment.NewLine + ex.ToString()); }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
