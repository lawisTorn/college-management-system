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
using CMS.Database;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using TheArtOfDevHtmlRenderer.Adapters;

namespace CMS.Admin
{
    public partial class adminTeacher : UserControl
    {
        public adminTeacher()
        {
            InitializeComponent();
        }

        private void adminTeacher_Load(object sender, EventArgs e)
        {
            UpdateDataBase();

            Database.DB db1 = new Database.DB();
            DataTable dt1 = new DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
            string gz = "SELECT `name` FROM `courses`";
            MySqlCommand group = new MySqlCommand(gz, db1.getConnection());
            adapter1.SelectCommand = group;
            adapter1.Fill(dt1);
            dataGridView2.DataSource = dt1;

            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                comboBox2.Items.Add(dataGridView2.Rows[i].Cells[0].Value);
            }
            comboBox2.Items.Add("Нет группы");

            DataTable dt2 = new DataTable();
            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
            string gz1 = "SELECT `name` FROM `subjects`";
            MySqlCommand group1 = new MySqlCommand(gz1, db1.getConnection());
            adapter2.SelectCommand = group1;
            adapter2.Fill(dt2);
            dataGridView3.DataSource = dt2;

            for (int i = 0; i < dataGridView3.RowCount - 1; i++)
            {
                comboBox1.Items.Add(dataGridView3.Rows[i].Cells[0].Value);
            }


        }

        public void UpdateDataBase() {
            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `id`, `fullname`, `address`, `email`, `number`, `subject`, `course`, `salary`, `dob`, `ed`, `login`, `password` FROM `teacher`", db.getConnection());
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
            dataGridView1.Columns[7].HeaderText = "Зарплата";
            dataGridView1.Columns[8].HeaderText = "Дата рождения";
            dataGridView1.Columns[9].HeaderText = "Дата Приема на работу";
            dataGridView1.Columns[10].HeaderText = "Логин";
            dataGridView1.Columns[11].HeaderText = "Пароль";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            guna2TextBox1.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox2.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox3.Text = Convert.ToString(dataGridView1[4, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox4.Text = Convert.ToString(dataGridView1[3, dataGridView1.CurrentRow.Index].Value);
            comboBox1.Text = Convert.ToString(dataGridView1[5, dataGridView1.CurrentRow.Index].Value);
            comboBox2.Text = Convert.ToString(dataGridView1[6, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox7.Text = Convert.ToString(dataGridView1[7, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox10.Text = Convert.ToString(dataGridView1[10, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox11.Text = Convert.ToString(dataGridView1[11, dataGridView1.CurrentRow.Index].Value);
            guna2DateTimePicker1.Text = Convert.ToString(dataGridView1[8, dataGridView1.CurrentRow.Index].Value);
            guna2DateTimePicker2.Text = Convert.ToString(dataGridView1[9, dataGridView1.CurrentRow.Index].Value);

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            guna2TextBox7.Text = "";
            guna2TextBox8.Text = "";
            guna2TextBox9.Text = "";
            guna2TextBox10.Text = "";
            guna2TextBox11.Text = "";
            guna2DateTimePicker1.Text = "";
            guna2DateTimePicker2.Text = "";
        }

        private void btn_editProfile_Click(object sender, EventArgs e)
        {
                string zapros = "INSERT INTO `teacher` (`id`, `fullname`, `address`, `email`, `number`, `subject`, `course`, `salary`, `dob`, `ed`, `login`, `password`) VALUES" +
                "(NULL, '"+guna2TextBox1.Text+"', '"+guna2TextBox2.Text+"', '"+guna2TextBox4.Text+"', '"+guna2TextBox3.Text+"', '"+comboBox1.Text+"'," +
                " '"+comboBox2.Text+"', '"+guna2TextBox7.Text+"', '"+guna2DateTimePicker1.Value.ToString("yyyy-MM-dd") +"', '"+guna2DateTimePicker2.Value.ToString("yyyy-MM-dd") +"'," +
                " '"+guna2TextBox10.Text+"', '"+guna2TextBox11.Text+"')";
            Database.DB db = new Database.DB();
            db.openConnection();
            MySqlCommand comm = new MySqlCommand(zapros, db.getConnection());
            comm.ExecuteNonQuery();
            db.closeConnection();

            UpdateDataBase();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
            int d = Convert.ToInt32(dataGridView1.Rows[id].Cells[0].Value);
            string del = "DELETE FROM `teacher` WHERE `id` = " + d + "";
            Database.DB db = new Database.DB();
            db.openConnection();
            MySqlCommand comm = new MySqlCommand(del, db.getConnection());
            comm.ExecuteNonQuery();
            db.closeConnection();

            UpdateDataBase();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
            int d = Convert.ToInt32(dataGridView1.Rows[id].Cells[0].Value);
            string zapros = "UPDATE `teacher` SET `fullname` = @fullname, `address` = @address, `email` = @email, `number` = @number, `subject` = @subject, `course` = @course, `salary` = @salary, `dob` = @dob, `ed` = @ed, `login` = @login, `password` = @password WHERE `id` = @id";

            Database.DB db = new Database.DB();
            db.openConnection();

            MySqlCommand comm = new MySqlCommand(zapros, db.getConnection());
            comm.Parameters.AddWithValue("@fullname", guna2TextBox1.Text);
            comm.Parameters.AddWithValue("@address", guna2TextBox2.Text);
            comm.Parameters.AddWithValue("@email", guna2TextBox4.Text);
            comm.Parameters.AddWithValue("@number", guna2TextBox3.Text);
            comm.Parameters.AddWithValue("@subject", comboBox1.Text);
            comm.Parameters.AddWithValue("@course", comboBox2.Text);
            comm.Parameters.AddWithValue("@salary", guna2TextBox7.Text);
            comm.Parameters.AddWithValue("@dob", guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"));
            comm.Parameters.AddWithValue("@ed", guna2DateTimePicker2.Value.ToString("yyyy-MM-dd"));
            comm.Parameters.AddWithValue("@login", guna2TextBox10.Text);
            comm.Parameters.AddWithValue("@password", guna2TextBox11.Text);
            comm.Parameters.AddWithValue("@id", d);

            comm.ExecuteNonQuery();
            db.closeConnection();

            UpdateDataBase();
        }

        private void guna2TextBox12_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            if (string.IsNullOrWhiteSpace(guna2TextBox12.Text))
                return;

            var values = guna2TextBox12.Text.Split(new char[] { ' ' },
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

        private void guna2Button4_Click(object sender, EventArgs e)
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
