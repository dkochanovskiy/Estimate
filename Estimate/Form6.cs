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

namespace Estimate
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "/Estimate.accdb");
        private void FillMethod()
        {
            try
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = con;
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM ColOrgPol", con);
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds, "ColOrgPol");
                dataGridView1.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка получения данных: " + Environment.NewLine + ex.ToString()); }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxOrg.Text != "" && textBoxCountUsers.Text != "" && textBoxActual.Text != "")
                {
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = con;
                    con.Open();
                    command.CommandText = "INSERT INTO ColOrgPol (Org, CountUsers, Actual) VAlUES ('" + textBoxOrg.Text + "', '" + textBoxCountUsers.Text + "', '" + textBoxActual.Text + "')";
                    command.ExecuteNonQuery();
                    con.Close();
                }
                else MessageBox.Show("Для добавления новой организации заполните текстовые поля!");
                FillMethod();
                textBoxOrg.Text = "";
                textBoxCountUsers.Text = "";
                textBoxActual.Text = "";
            }
            catch (Exception ex) { MessageBox.Show("Ошибка получения данных: " + Environment.NewLine + ex.ToString()); }
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            FillMethod();
        }
    }
}
