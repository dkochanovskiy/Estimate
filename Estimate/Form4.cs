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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        string connectionString = "Database=estimate;Data Source=localhost;User Id=root;Password=";
        string combo;
        private void Fill()
        {
            try
            {
                FillSections();
                FillEdIzmer();
            }
            catch { /*MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString());*/ }
        }
        private void FillSections()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT `Section_Name` FROM Sections";
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(query, connection);
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                comboBoxChoice.DataSource = table;
                comboBoxChoice.DisplayMember = "Section_Name";
                comboBoxChoice.ValueMember = "Section_Name";

                connection.Close();
            }
            catch { /*MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString());*/ }
        }
        private void FillEdIzmer()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM EdIzmer";
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(query, connection);
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                comboBoxEdIzmer.DataSource = table;
                comboBoxEdIzmer.DisplayMember = "EdIzmer";
                comboBoxEdIzmer.ValueMember = "EdIzmer";

                connection.Close();
            }
            catch { /*MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString());*/ }
        }
        private void FillDGV()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT `code`, `rabota`, `edizmer` FROM " + combo;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter);
                DataSet ds = new DataSet();
                adapter.SelectCommand = command;
                adapter.Fill(ds, combo);

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 1200;

                connection.Close();
            }
            catch { /*MessageBox.Show("Ошибка получения данных: " + Environment.NewLine + ex.ToString());*/ }
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                Fill();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка получения данных: " + Environment.NewLine + ex.ToString()); }
        }
        private void comboBoxChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                combo = comboBoxChoice.Text;
                FillDGV();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка получения данных: " + Environment.NewLine + ex.ToString()); }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxCode.Text != "" && textBoxPerRab.Text != "")
                {
                    MySqlConnection connection = new MySqlConnection(connectionString);

                    connection.Open();
                    string query = "INSERT INTO " + combo + " (code, rabota, edizmer) VAlUES ('" + textBoxCode.Text + "','" + textBoxPerRab.Text + "','" + comboBoxEdIzmer.Text + "')";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                else MessageBox.Show("Для добавления новой работы заполните текстовые поля!");
                FillDGV();
                textBoxPerRab.Text = "";
                textBoxCode.Text = "";
            }
            catch (Exception ex) { MessageBox.Show("Ошибка получения данных: " + Environment.NewLine + ex.ToString()); }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}