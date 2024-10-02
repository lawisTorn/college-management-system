using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;

namespace CMS.Teacher
{
    public partial class teacherTeacher : UserControl
    {
        public teacherTeacher()
        {
            InitializeComponent();
        }

        private void teacherTeacher_Load(object sender, EventArgs e)
        {
            UpdateDataBase();
            comboBox2.Items.Add("Нет");
            comboBox2.Items.Add("Нет группы");
            comboBox1.Items.Add("Нет");
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            for (int i = 0; i < dataGridView3.RowCount - 1; i++)
            {
                comboBox1.Items.Add(dataGridView3.Rows[i].Cells[0].Value);
            }

            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                comboBox2.Items.Add(dataGridView2.Rows[i].Cells[0].Value);
            }
        }

        public void UpdateDataBase()
        {
            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `id`, `fullname`, `address`, `email`, `number`, `subject`, `course`, `dob`, `ed` FROM `teacher`", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Код";
            dataGridView1.Columns[1].HeaderText = "Полное ФИО";
            dataGridView1.Columns[2].HeaderText = "Адрес";
            dataGridView1.Columns[3].HeaderText = "Почта";
            dataGridView1.Columns[4].HeaderText = "Телефон";
            dataGridView1.Columns[5].HeaderText = "Предмет";
            dataGridView1.Columns[6].HeaderText = "Группа";
            dataGridView1.Columns[7].HeaderText = "Дата рождения";
            dataGridView1.Columns[8].HeaderText = "Дата Приема на работу";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            DataTable dt2 = new DataTable();
            MySqlCommand command2 = new MySqlCommand("SELECT `name` FROM `courses`", db.getConnection());
            adapter.SelectCommand = command2;
            adapter.Fill(dt2);
            dataGridView2.DataSource = dt2;

            DataTable dt3 = new DataTable();
            MySqlCommand command3 = new MySqlCommand("SELECT `name` FROM `subjects`", db.getConnection());
            adapter.SelectCommand = command3;
            adapter.Fill(dt3);
            dataGridView3.DataSource = dt3;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            Database.DB db = new Database.DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            string zapros_1 = "SELECT `id`, `fullname`, `address`, `email`, `number`, `subject`, `course`, `dob`, `ed` FROM `teacher` WHERE `subject` = '"+Convert.ToString(comboBox1.Text)+"'";
            string zapros_2 = "SELECT `id`, `fullname`, `address`, `email`, `number`, `subject`, `course`, `dob`, `ed` FROM `teacher`";
            string zapros_3 = "SELECT `id`, `fullname`, `address`, `email`, `number`, `subject`, `course`, `dob`, `ed` FROM `teacher` WHERE `subject` = '" + Convert.ToString(comboBox1.Text) + "' AND `course` = '" + Convert.ToString(comboBox2.Text) + "'";
            string zapros_4 = "SELECT `id`, `fullname`, `address`, `email`, `number`, `subject`, `course`, `dob`, `ed` FROM `teacher` WHERE `course` = '" + Convert.ToString(comboBox2.Text) + "'";


            if (comboBox1.SelectedIndex != 0)
            {
                if (comboBox2.SelectedIndex != 0)
                {
                    MySqlCommand command1 = new MySqlCommand(zapros_3, db.getConnection());
                    adapter.SelectCommand = command1;
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                else if (comboBox2.SelectedIndex == 0)
                {
                    MySqlCommand command = new MySqlCommand(zapros_1, db.getConnection());
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            else if (comboBox1.SelectedIndex == 0) 
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    UpdateDataBase();
                }
                else if (comboBox2.SelectedIndex != 0) {
                    MySqlCommand command1 = new MySqlCommand(zapros_4, db.getConnection());
                    adapter.SelectCommand = command1;
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.DB db = new Database.DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            string zapros_1 = "SELECT `id`, `fullname`, `address`, `email`, `number`, `subject`, `course`, `dob`, `ed` FROM `teacher` WHERE `subject` = '" + Convert.ToString(comboBox1.Text) + "'";
            string zapros_2 = "SELECT `id`, `fullname`, `address`, `email`, `number`, `subject`, `course`, `dob`, `ed` FROM `teacher`";
            string zapros_3 = "SELECT `id`, `fullname`, `address`, `email`, `number`, `subject`, `course`, `dob`, `ed` FROM `teacher` WHERE `subject` = '" + Convert.ToString(comboBox1.Text) + "' AND `course` = '" + Convert.ToString(comboBox2.Text) + "'";
            string zapros_4 = "SELECT `id`, `fullname`, `address`, `email`, `number`, `subject`, `course`, `dob`, `ed` FROM `teacher` WHERE `course` = '" + Convert.ToString(comboBox2.Text) + "'";


            if (comboBox2.SelectedIndex != 0)
            {
                if (comboBox1.SelectedIndex != 0)
                {
                    MySqlCommand command1 = new MySqlCommand(zapros_3, db.getConnection());
                    adapter.SelectCommand = command1;
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                else if (comboBox1.SelectedIndex == 0)
                {
                    MySqlCommand command = new MySqlCommand(zapros_4, db.getConnection());
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            else if (comboBox2.SelectedIndex == 0)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    UpdateDataBase();
                }
                else if (comboBox1.SelectedIndex != 0)
                {
                    MySqlCommand command1 = new MySqlCommand(zapros_1, db.getConnection());
                    adapter.SelectCommand = command1;
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            string email = Convert.ToString(dataGridView1[3, dataGridView1.CurrentRow.Index].Value);
            string subject = Convert.ToString(dataGridView1[5, dataGridView1.CurrentRow.Index].Value);
            string dob = Convert.ToString(dataGridView1[7, dataGridView1.CurrentRow.Index].Value);
            string ed = Convert.ToString(dataGridView1[8, dataGridView1.CurrentRow.Index].Value);

            label19.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
            label18.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentRow.Index].Value);
            label17.Text = Convert.ToString(dataGridView1[4, dataGridView1.CurrentRow.Index].Value);
            label16.Text = TruncateText(email, 23);
            label15.Text = dob.Length >= 7 ? dob.Substring(0, dob.Length - 7) : dob;
            label12.Text = ed.Length >= 7 ? ed.Substring(0, ed.Length - 7) : ed;
            label14.Text = TruncateText(subject, 15);
            label13.Text = Convert.ToString(dataGridView1[6, dataGridView1.CurrentRow.Index].Value);
        }
        private string TruncateText(string text, int maxLength)
        {
            if (text.Length > maxLength)
            {
                return text.Substring(0, maxLength) + "...";
            }
            return text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            if (string.IsNullOrWhiteSpace(guna2TextBox4.Text))
                return;

            var values = guna2TextBox4.Text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                foreach (string value in values)
                {
                    var row = dataGridView1.Rows[i];

                    if (row.Cells[1].Value.ToString().Contains(value))
                    {
                        row.Selected = true;
                    }
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Установка контекста лицензии
                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                // Создание нового Excel файла
                using (var package = new ExcelPackage())
                {
                    // Добавление нового рабочего листа
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    // Добавление заголовков столбцов
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = dataGridView1.Columns[i].HeaderText;
                    }

                    // Добавление данных из DataGridView
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                        }
                    }
                    // Автоматическое изменение размера столбцов
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Открытие диалога сохранения файла
                    using (var saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xlsx";
                        saveFileDialog.Title = "Save as Excel File";
                        saveFileDialog.FileName = "teacher.xlsx";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            var file = new FileInfo(saveFileDialog.FileName);
                            package.SaveAs(file);
                            MessageBox.Show("Export completed successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
