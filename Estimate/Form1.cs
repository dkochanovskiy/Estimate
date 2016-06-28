using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using Microsoft.Office.Interop.Word;
using DataTable = System.Data.DataTable;
using System.Data.Sql;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Estimate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string connectionString = "Database=estimate;Data Source=localhost;User Id=root;Password=";

        string code;
        int countRows;
        int i = 0;
        Excel.Application exApp = new Excel.Application();
        string mySheet = AppDomain.CurrentDomain.BaseDirectory + @"\template.xlsx";
        string naCoef;
        int itog;
        private void FillAIS()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM AIS";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(query, connection);
            DataTable table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            comboBoxAIS.DataSource = table;
            comboBoxAIS.DisplayMember = "AIS_Name";
            comboBoxAIS.ValueMember = "AIS_Name";

            connection.Close();
        }
        private void FillSections()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM Sections";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(query, connection);
            DataTable table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            comboBoxChangeSection.DataSource = table;
            comboBoxChangeSection.DisplayMember = "Section_Name";
            comboBoxChangeSection.ValueMember = "Section_Name";

            connection.Close();
        }

        private void FillDGV()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT `estimate_name`, `create_date` FROM `estimates_meta`";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.SelectCommand = command;
            adapter.Fill(ds, "estimates_meta");

            dataGridView3.DataSource = ds.Tables[0];
            dataGridView3.Columns[0].Width = 1300;
            dataGridView3.Columns[1].Width = 150;

            connection.Close();
        }
        private void ClearAll()
        {
            try
            {
                textBoxNormaVremeni.Text = "";
                textBoxGodSredneStat.Text = "";
                textBoxKol.Text = "";
                textBoxComment.Text = "";
                textBoxCoefficient.Text = "";
                FillAIS();
                FillSections();
                comboBoxType.Text = "Выберите тип сметы";

                if (dataGridView1.RowCount != 0)
                {
                    for (int i = 0; i <= dataGridView1.RowCount; i++)
                    {
                        dataGridView1.Rows.RemoveAt(dataGridView1.RowCount - 1);
                        i = 0;
                    }
                }
                if (dataGridView2.RowCount != 0)
                {
                    for (int i = 0; i <= dataGridView2.RowCount; i++)
                    {
                        dataGridView2.Rows.RemoveAt(dataGridView2.RowCount - 1);

                        i = 0;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Ошибка получения данных: " + Environment.NewLine + ex.ToString()); }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                FillAIS();
                FillSections();
                FillDGV();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }
        private void AISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form2 form = new Form2();
                form.Show();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }
        private void razdelyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form3 form = new Form3();
                form.Show();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }
        private void perechenRabotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form4 form = new Form4();
                form.Show();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }
        private void edIzmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form5 form = new Form5();
                form.Show();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }
        private void colOrgPolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form6 form = new Form6();
                form.Show();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }
        private void comboBoxChangeSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string combo = comboBoxChangeSection.Text;

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
            catch { /*MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString());*/ }
            //catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }
        private void открытьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
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

                dataGridView2.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 1354;

                connection.Close();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }

        private void сохранитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);

                if (textBoxSaveAisName.Text != "")
                {
                    string type = comboBoxType.Text;
                    string ais = comboBoxAIS.Text;
                    string year = DateTime.Now.Year.ToString();
                    string month = DateTime.Now.Month.ToString();

                    if (month == "1") month = "Январь";
                    else if (month == "2") month = "Февраль";
                    else if (month == "3") month = "Март";
                    else if (month == "4") month = "Апрель";
                    else if (month == "5") month = "Май";
                    else if (month == "6") month = "Июнь";
                    else if (month == "7") month = "Июль";
                    else if (month == "8") month = "Август";
                    else if (month == "9") month = "Сентябрь";
                    else if (month == "10") month = "Октябрь";
                    else if (month == "11") month = "Ноябрь";
                    else if (month == "12") month = "Декабрь";

                    string day = DateTime.Now.Day.ToString();
                    string hour = DateTime.Now.Hour.ToString();
                    string minute = DateTime.Now.Minute.ToString();
                    string second = DateTime.Now.Second.ToString();

                    connection.Open();
                    string query_create = "CREATE TABLE IF NOT EXISTS `" + textBoxSaveAisName.Text + "`(`id` int(11) NOT NULL AUTO_INCREMENT, `column_1` varchar(127), `column_2` varchar(127), `column_3` varchar(127), `column_4` varchar(127), `column_5` varchar(127), `column_6` varchar(127), `column_7` varchar(127), `column_8` varchar(127), `column_9` varchar(127), `column_10` varchar(127), `date` datetime, PRIMARY KEY (`id`))";
                    MySqlCommand command_create = new MySqlCommand(query_create, connection);
                    command_create.ExecuteNonQuery();
                    connection.Close();


                    connection.Open();

                    string query_meta = "INSERT INTO `estimates_meta` (`id`, `estimate_name`, `type`, `ais`, `year`, `month`, `day`, `hour`, `minute`, `second`, `create_date`) VAlUES ('"
                        + null
                        + "', '" + textBoxSaveAisName.Text
                        + "', '" + type
                        + "', '" + ais
                        + "', '" + year
                        + "', '" + month
                        + "', '" + day
                        + "', '" + hour
                        + "', '" + minute
                        + "', '" + second + "', CURRENT_TIMESTAMP)";
                    MySqlCommand command_meta = new MySqlCommand(query_meta, connection);
                    command_meta.ExecuteNonQuery();
                    connection.Close();


                    connection.Open();

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        string query = "INSERT INTO `" + textBoxSaveAisName.Text + "` (`id`, `column_1`, `column_2`, `column_3`, `column_4`, `column_5`, `column_6`, `column_7`, `column_8`, `column_9`, `column_10`, `date`) VAlUES ('"
                        + null
                        + "', '" + dataGridView2.Rows[i].Cells[0].Value.ToString()
                        + "', '" + dataGridView2.Rows[i].Cells[1].Value.ToString()
                        + "', '" + dataGridView2.Rows[i].Cells[2].Value.ToString()
                        + "', '" + dataGridView2.Rows[i].Cells[3].Value.ToString()
                        + "', '" + dataGridView2.Rows[i].Cells[4].Value.ToString()
                        + "', '" + dataGridView2.Rows[i].Cells[5].Value.ToString()
                        + "', '" + dataGridView2.Rows[i].Cells[6].Value.ToString()
                        + "', '" + dataGridView2.Rows[i].Cells[7].Value.ToString()
                        + "', '" + dataGridView2.Rows[i].Cells[8].Value.ToString()
                        + "', '" + dataGridView2.Rows[i].Cells[9].Value.ToString() + "', CURRENT_TIMESTAMP)";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    FillDGV();
                }
                else
                {
                    MessageBox.Show("Введите название для сохраняемой сметы");
                }
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }

        private void обновитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                FillAIS();
                FillSections();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }

        private void добавитьРазделToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = "NewRazdel";
                dataGridView2.Rows[i].Cells[1].Value = comboBoxChangeSection.Text;
                dataGridView2.Rows[i].Cells[2].Value = "";
                dataGridView2.Rows[i].Cells[3].Value = "";
                dataGridView2.Rows[i].Cells[4].Value = "";
                dataGridView2.Rows[i].Cells[5].Value = 0;
                dataGridView2.Rows[i].Cells[6].Value = "";
                dataGridView2.Rows[i].Cells[7].Value = 0;
                dataGridView2.Rows[i].Cells[8].Value = "";
                dataGridView2.Rows[i].Cells[9].Value = "";
                i++;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }

        private void добавитьКСметеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                int normaVremeni = Convert.ToInt16(textBoxNormaVremeni.Text);
                int godSredneStat = Convert.ToInt16(textBoxGodSredneStat.Text);
                int godNormZatratNaObsEd = normaVremeni * godSredneStat;
                int kolObsEd = Convert.ToInt16(textBoxKol.Text);
                int godNormZatrat = kolObsEd * godNormZatratNaObsEd;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = dataGridView1.SelectedCells[0].Value.ToString().Replace(" ", string.Empty);
                dataGridView2.Rows[i].Cells[1].Value = dataGridView1.SelectedCells[1].Value.ToString().Replace(" ", string.Empty);
                dataGridView2.Rows[i].Cells[2].Value = dataGridView1.SelectedCells[2].Value.ToString().Replace(" ", string.Empty);
                dataGridView2.Rows[i].Cells[3].Value = textBoxNormaVremeni.Text.Replace(" ", string.Empty);
                dataGridView2.Rows[i].Cells[4].Value = textBoxGodSredneStat.Text.Replace(" ", string.Empty);
                dataGridView2.Rows[i].Cells[5].Value = godNormZatratNaObsEd;
                dataGridView2.Rows[i].Cells[6].Value = textBoxKol.Text.Replace(" ", string.Empty);
                dataGridView2.Rows[i].Cells[7].Value = godNormZatrat;
                dataGridView2.Rows[i].Cells[8].Value = textBoxComment.Text.Replace(" ", string.Empty);
                if (textBoxCoefficient.Text != "")
                {
                    int coef = Convert.ToInt16(textBoxCoefficient.Text);
                    int naCoef = godNormZatrat * coef;
                    dataGridView2.Rows[i].Cells[9].Value = naCoef;
                }
                else
                {
                    dataGridView2.Rows[i].Cells[9].Value = "";
                }
                i++;
                //label1.Text = i.ToString();
                textBoxNormaVremeni.Text = "";
                textBoxGodSredneStat.Text = "";
                textBoxKol.Text = "";
                textBoxComment.Text = "";
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }

        private void итогоПоРазделуToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                int itogNaEd = 0;
                int itogGodNorm = 0;
                for (int j = dataGridView2.RowCount; 0 <= j; j--)
                {
                    itogNaEd += Convert.ToInt16(dataGridView2.Rows[j - 1].Cells[5].Value);
                    itogGodNorm += Convert.ToInt16(dataGridView2.Rows[j - 1].Cells[7].Value);
                    if (dataGridView2.Rows[j - 1].Cells[0].Value.ToString() == "NewRazdel")
                        break;
                }
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = "";
                dataGridView2.Rows[i].Cells[1].Value = "Итого по разделу";
                dataGridView2.Rows[i].Cells[2].Value = "X";
                dataGridView2.Rows[i].Cells[3].Value = "X";
                dataGridView2.Rows[i].Cells[4].Value = "X";
                dataGridView2.Rows[i].Cells[5].Value = itogNaEd.ToString();
                dataGridView2.Rows[i].Cells[6].Value = "X";
                dataGridView2.Rows[i].Cells[7].Value = itogGodNorm.ToString();
                dataGridView2.Rows[i].Cells[8].Value = "";
                dataGridView2.Rows[i].Cells[9].Value = "";
                i++;
                //label1.Text = i.ToString();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }

        private void итогоToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                itog = 0;
                for (int c = 0; c < dataGridView2.RowCount; c++)
                {
                    if (dataGridView2.Rows[c].Cells[1].Value.ToString() == "Итого по разделу")
                        itog += Convert.ToInt16(dataGridView2.Rows[c].Cells[7].Value);
                }
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = "";
                dataGridView2.Rows[i].Cells[1].Value = "Итого";
                dataGridView2.Rows[i].Cells[2].Value = "X";
                dataGridView2.Rows[i].Cells[3].Value = "X";
                dataGridView2.Rows[i].Cells[4].Value = "X";
                dataGridView2.Rows[i].Cells[5].Value = "X";
                dataGridView2.Rows[i].Cells[6].Value = "X";
                dataGridView2.Rows[i].Cells[7].Value = itog.ToString();
                dataGridView2.Rows[i].Cells[8].Value = "X";
                dataGridView2.Rows[i].Cells[9].Value = "X";
                i++;
                //label1.Text = i.ToString();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }

        private void создатьСметуToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                var excelApp = new Excel.Application();
                Excel.Workbooks books = excelApp.Workbooks;
                Excel.Workbook sheet = books.Open(mySheet);
                Worksheet workSheet = (Worksheet)excelApp.ActiveSheet;
                countRows = dataGridView1.SelectedRows.Count;
                excelApp.Cells[1, 1] = comboBoxType.Text + " смета: Сумма трудоемкости работ, выполняемых  ГАУ РМ «Госинформ», связанных с обслуживанием АИС «" + comboBoxAIS.Text + "»";
                Excel.Range oRange;
                oRange = workSheet.Range[workSheet.Cells[4, 1], workSheet.Cells[4, 9]];
                oRange.Merge(Type.Missing);
                oRange = workSheet.Range[workSheet.Cells[i + 5, 2], workSheet.Cells[i + 5, 6]];
                oRange.Merge(Type.Missing);
                (workSheet.Cells[i + 3, 2] as Excel.Range).Font.Name = "Times new roman";
                (workSheet.Cells[i + 3, 2] as Excel.Range).Font.Bold = true;
                (workSheet.Cells[i + 3, 2] as Excel.Range).Font.Size = 11;
                (workSheet.Cells[i + 4, 2] as Excel.Range).Font.Name = "Times new roman";
                (workSheet.Cells[i + 4, 2] as Excel.Range).Font.Bold = true;
                (workSheet.Cells[i + 4, 2] as Excel.Range).Font.Size = 12;
                excelApp.Cells[i + 4, 2] = "ИТОГО: сумма трудоемкости работ, выполняемых  ГАУ РМ «Госинформ», связанных с обслуживанием АИС «" + comboBoxAIS.Text + "»";
                (workSheet.Cells[i + 4, 8] as Excel.Range).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                excelApp.Cells[i + 4, 8] = itog;
                (workSheet.Cells[i + 4, 9] as Excel.Range).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                (workSheet.Cells[i + 4, 9] as Excel.Range).Font.Name = "Times new roman";
                excelApp.Cells[i + 4, 9] = "X";
                int rowExcel = 4;
                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                {
                    if (dataGridView2.Rows[j].Cells[0].Value.ToString() == "NewRazdel")
                    {
                        oRange = workSheet.Range[workSheet.Cells[rowExcel, 1], workSheet.Cells[rowExcel, 9]];
                        (workSheet.Cells[rowExcel, 1] as Excel.Range).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        (workSheet.Cells[rowExcel, 1] as Excel.Range).Font.Bold = true;
                        (workSheet.Cells[rowExcel, 1] as Excel.Range).Font.Size = 11;
                        (workSheet.Cells[rowExcel, 1] as Excel.Range).Font.Name = "Times new roman";
                        oRange.Merge(Type.Missing);
                        workSheet.Cells[rowExcel, 1] = dataGridView2.Rows[j].Cells[1].Value.ToString();
                        ++rowExcel;
                    }
                    else
                    {
                        code = dataGridView2.Rows[j].Cells[0].Value.ToString();
                        string rabota = dataGridView2.Rows[j].Cells[1].Value.ToString();
                        string edIzmer = dataGridView2.Rows[j].Cells[2].Value.ToString();
                        string norma = dataGridView2.Rows[j].Cells[3].Value.ToString();
                        string godSredneStat = dataGridView2.Rows[j].Cells[4].Value.ToString();
                        string godNormaZatrat = dataGridView2.Rows[j].Cells[5].Value.ToString();
                        string kol = dataGridView2.Rows[j].Cells[6].Value.ToString();
                        string god = dataGridView2.Rows[j].Cells[7].Value.ToString();
                        string comment = dataGridView2.Rows[j].Cells[8].Value.ToString();
                        if (dataGridView2.Rows[j].Cells[9].Value != null)
                        {
                            naCoef = dataGridView2.Rows[j].Cells[9].Value.ToString();
                        }
                        (workSheet.Cells[rowExcel, "A"] as Excel.Range).Font.Name = "Times new roman";
                        workSheet.Cells[rowExcel, "A"] = code;
                        (workSheet.Cells[rowExcel, "B"] as Excel.Range).Font.Name = "Times new roman";
                        workSheet.Cells[rowExcel, "B"] = rabota;
                        (workSheet.Cells[rowExcel, "C"] as Excel.Range).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        (workSheet.Cells[rowExcel, "C"] as Excel.Range).Font.Name = "Times new roman";
                        workSheet.Cells[rowExcel, "C"] = edIzmer;
                        (workSheet.Cells[rowExcel, "D"] as Excel.Range).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        (workSheet.Cells[rowExcel, "D"] as Excel.Range).Font.Name = "Times new roman";
                        workSheet.Cells[rowExcel, "D"] = norma;
                        (workSheet.Cells[rowExcel, "E"] as Excel.Range).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        (workSheet.Cells[rowExcel, "E"] as Excel.Range).Font.Name = "Times new roman";
                        workSheet.Cells[rowExcel, "E"] = godSredneStat;
                        (workSheet.Cells[rowExcel, "F"] as Excel.Range).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        (workSheet.Cells[rowExcel, "F"] as Excel.Range).Font.Name = "Times new roman";
                        workSheet.Cells[rowExcel, "F"] = godNormaZatrat;
                        (workSheet.Cells[rowExcel, "G"] as Excel.Range).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        (workSheet.Cells[rowExcel, "G"] as Excel.Range).Font.Name = "Times new roman";
                        workSheet.Cells[rowExcel, "G"] = kol;
                        (workSheet.Cells[rowExcel, "H"] as Excel.Range).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        (workSheet.Cells[rowExcel, "H"] as Excel.Range).Font.Name = "Times new roman";
                        workSheet.Cells[rowExcel, "H"] = god;
                        (workSheet.Cells[rowExcel, "I"] as Excel.Range).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        (workSheet.Cells[rowExcel, "I"] as Excel.Range).Font.Name = "Times new roman";
                        workSheet.Cells[rowExcel, "I"] = comment;
                        (workSheet.Cells[rowExcel, "J"] as Excel.Range).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        (workSheet.Cells[rowExcel, "J"] as Excel.Range).Font.Name = "Times new roman";
                        workSheet.Cells[rowExcel, "J"] = naCoef;

                        ++rowExcel;
                    }
                }
                excelApp.Visible = true;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка! " + Environment.NewLine + ex.ToString()); }
        }

        private void удалитьСтрокуToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.RowCount != 0)
                {
                    dataGridView2.Rows.RemoveAt(dataGridView2.RowCount - 1);
                    i--;
                }
                //label1.Text = i.ToString();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка получения данных: " + Environment.NewLine + ex.ToString()); }
        }

        private void очиститьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView3.CurrentRow.Cells[0].Value.ToString());
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT `column_1`, `column_2`, `column_3`, `column_4`, `column_5`, `column_6`, `column_7`, `column_8`, `column_9`, `column_10` FROM `" + dataGridView3.CurrentRow.Cells[0].Value.ToString() + "`";
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter);
                DataSet ds = new DataSet();
                adapter.SelectCommand = command;
                adapter.Fill(ds, "" + dataGridView3.CurrentRow.Cells[0].Value + "");

                
                dataGridView2.DataSource = ds.Tables[0];

                connection.Close();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка получения данных: " + Environment.NewLine + ex.ToString()); }
        }
    }
}