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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        string connectionString = "Database=estimate;Data Source=localhost;User Id=root;Password=";
        private void Form3_Load(object sender, EventArgs e)
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

            string query = "SELECT `Section_Name` FROM Sections";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.SelectCommand = command;
            adapter.Fill(ds, "Section_Name");

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 1354;

            connection.Close();
        }

        private void FillSection(string section_Name)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO Sections (Section_Name) VAlUES ('" + section_Name + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void CreateTable(string section_Name)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            //string query = "CREATE TABLE " + section_Name + " (Work_id char, Code char, Rabota char, EdIzmer char)";
            string query = "CREATE TABLE IF NOT EXISTS `" + section_Name + "` (`id` int(11) NOT NULL AUTO_INCREMENT, `code` varchar(128) COLLATE utf8_unicode_ci NOT NULL, `rabota` varchar(255) COLLATE utf8_unicode_ci NOT NULL, `edizmer` varchar(128) COLLATE utf8_unicode_ci NOT NULL, PRIMARY KEY (`id`)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string str = textBoxRazdel.Text;
                str = str.Replace(" ", "_");
                if (textBoxRazdel.Text != "")
                {
                    FillSection(str);
                    CreateTable(str);
                }
                else MessageBox.Show("Для добавления нового раздела заполните текстовые поля!");
                FillMethod();
                textBoxRazdel.Text = "";
            }
            catch { /*MessageBox.Show("Ошибка получения данных: " + Environment.NewLine + ex.ToString());*/ }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
