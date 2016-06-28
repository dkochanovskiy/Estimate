using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Estimate
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        string connectionString = "Database=estimate;Data Source=localhost;User Id=root;Password=";
        private void Form5_Load(object sender, EventArgs e)
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

            string query = "SELECT `EdIzmer` FROM EdIzmer";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.SelectCommand = command;
            adapter.Fill(ds, "EdIzmer");

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 1450;

            connection.Close();
        }
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);

                if (textBoxEdIzm.Text != "")
                {
                    connection.Open();
                    string query = "INSERT INTO EdIzmer (EdIzmer) VAlUES ('" + textBoxEdIzm.Text + "')";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                else MessageBox.Show("Для добавления новой единицы измерения заполните текстовое поле!");
                FillMethod();
                textBoxEdIzm.Text = "";
            }
            catch (Exception ex) { MessageBox.Show("Ошибка получения данных: " + Environment.NewLine + ex.ToString()); }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
