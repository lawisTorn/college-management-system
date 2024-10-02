using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CMS.Database;
using MySql.Data.MySqlClient;
using TheArtOfDevHtmlRenderer.Adapters;
using OfficeOpenXml;
using System.IO;

namespace CMS.Teacher
{
    public partial class teacherGrade : UserControl
    {
        public teacherGrade()
        {
            InitializeComponent();
        }

        private void teacherGrade_Load(object sender, EventArgs e)
        {
            courseTable();
            guna2DateTimePicker1.Value = DateTime.Today;
            guna2DateTimePicker2.Value = DateTime.Today;
            guna2DateTimePicker3.Value = DateTime.Today;

            for (int i = 0; i < dataGridView3.RowCount - 1; i++)
            {
                comboBox1.Items.Add(dataGridView3.Rows[i].Cells[0].Value);
            }

            comboBox1.SelectedIndex = 0;

            UpdateDatabase();

            comboBox2.Items.Add("A");
            comboBox2.Items.Add("B");
            comboBox2.Items.Add("C");
            comboBox2.Items.Add("D");
            comboBox2.Items.Add("E");

            comboBox2.SelectedIndex = 0;
        }

        private void courseTable() {
            Database.DB db = new Database.DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT `name` FROM `courses`", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            dataGridView3.DataSource = dt;

            DataTable dt2 = new DataTable();
            MySqlCommand command2 = new MySqlCommand("SELECT `subject` FROM `teacher` WHERE `fullname` = '"+label1.Text+"'", db.getConnection());
            adapter.SelectCommand = command2;
            adapter.Fill(dt2);
            dataGridView4.DataSource = dt2;
        }
        private void UpdateDatabase() {
            string datetime_start = Convert.ToString(guna2DateTimePicker2.Value.ToString("yyyy-MM-dd"));
            string datetime_end = Convert.ToString(guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"));
            if (guna2DateTimePicker2.Value > guna2DateTimePicker1.Value)
            {
                MessageBox.Show("Некоректная дата!", "Ошибка дат", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if ((guna2DateTimePicker2.Value == guna2DateTimePicker1.Value) || (guna2DateTimePicker2.Value < guna2DateTimePicker1.Value))
            {
                Database.DB db = new Database.DB();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                MySqlCommand command = new MySqlCommand("SELECT `id`, `subject`, `name_student`, `rating`, `name_teacher`, `course`, `date` FROM `grade` WHERE `name_teacher` = '" + label1.Text + "' AND `course` = '" + comboBox1.Text + "' AND `date` BETWEEN '" + datetime_start + "' AND '" + datetime_end + "'", db.getConnection());
                adapter.SelectCommand = command;
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "Код";
                dataGridView1.Columns[1].HeaderText = "Предмет";
                dataGridView1.Columns[2].HeaderText = "Студент";
                dataGridView1.Columns[3].HeaderText = "Оценка";
                dataGridView1.Columns[4].HeaderText = "Учитель";
                dataGridView1.Columns[5].HeaderText = "Группа";
                dataGridView1.Columns[6].HeaderText = "Дата";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                DataTable dt2 = new DataTable();

                MySqlCommand command2 = new MySqlCommand("SELECT `id`, `fullname`, `course` FROM `student` WHERE `course` = '" + comboBox1.Text + "'", db.getConnection());
                adapter.SelectCommand = command2;
                adapter.Fill(dt2);
                dataGridView2.DataSource = dt2;
                dataGridView2.Columns[0].HeaderText = "Код";
                dataGridView2.Columns[1].HeaderText = "ФИО";
                dataGridView2.Columns[2].HeaderText = "Группа";
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDatabase();
        }

        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            UpdateDatabase();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string date = Convert.ToString(guna2DateTimePicker3.Value.ToString("yyyy-MM-dd"));
            string name_s = Convert.ToString(dataGridView2[1, dataGridView2.CurrentRow.Index].Value);
            string name_t = label1.Text;
            string subject = Convert.ToString(dataGridView4[0,0].Value);
            string course = Convert.ToString(dataGridView2[2, dataGridView2.CurrentRow.Index].Value);
            string rating = comboBox2.Text;


            string zapros = "INSERT INTO `grade` (`id`, `subject`, `name_student`, `rating`, `name_teacher`, `course`, `date`) VALUES (NULL, @subject, @name_s, @rating, @name_t, @course, @date)";

            Database.DB db = new Database.DB();
            MySqlCommand command = new MySqlCommand(zapros, db.getConnection());
            command.Parameters.Add("@subject", MySqlDbType.Text).Value = subject;
            command.Parameters.Add("@name_s", MySqlDbType.Text).Value = name_s;
            command.Parameters.Add("@rating", MySqlDbType.Text).Value = rating;
            command.Parameters.Add("@name_t", MySqlDbType.Text).Value = name_t;
            command.Parameters.Add("@course", MySqlDbType.Text).Value = course;
            command.Parameters.Add("@date", MySqlDbType.DateTime).Value = date;
            db.openConnection();
            command.ExecuteNonQuery();
            db.closeConnection();

            UpdateDatabase();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
            int d = Convert.ToInt32(dataGridView1.Rows[id].Cells[0].Value);
            string del = "DELETE FROM `grade` WHERE `id` = " + d + "";
            Database.DB db = new Database.DB();
            db.openConnection();
            MySqlCommand comm = new MySqlCommand(del, db.getConnection());
            comm.ExecuteNonQuery();
            db.closeConnection();

            UpdateDatabase();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
                return;

            var values = guna2TextBox1.Text.Split(new char[] { ' ' },
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

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();

            if (string.IsNullOrWhiteSpace(guna2TextBox4.Text))
                return;

            var values = guna2TextBox4.Text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                foreach (string value in values)
                {
                    var row = dataGridView2.Rows[i];

                    if (row.Cells[1].Value.ToString().Contains(value))
                    {
                        row.Selected = true;
                    }
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
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
                        saveFileDialog.FileName = "grade.xlsx";

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

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            UpdateDatabase();
        }
    }
}
