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
using Google.Protobuf.WellKnownTypes;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;

namespace CMS.Admin
{
    public partial class adminStudent : UserControl
    {
        public adminStudent()
        {
            InitializeComponent();
        }

        private void adminStudent_Load(object sender, EventArgs e)
        {
            UpdateDatabase();

            comboBox2.Items.Add("М");
            comboBox2.Items.Add("Ж");
            comboBox2.SelectedIndex = 0;

            guna2TextBox7.Text = "0";
            guna2TextBox7.FillColor = Color.Silver;

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
                comboBox1.Items.Add(dataGridView2.Rows[i].Cells[0].Value);
            }
        }

        private void UpdateDatabase() {
            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet dataSet = new DataSet();
            MySqlCommand command = new MySqlCommand("SELECT `id`, `fullname`, `address`, `email`, `number`, `course`, `dob`, `ed`, `dl`, `salary`, `login`, `password`, `sex` FROM `student`", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Код";
            dataGridView1.Columns[1].HeaderText = "Полное ФИО";
            dataGridView1.Columns[2].HeaderText = "Адрес";
            dataGridView1.Columns[3].HeaderText = "Почта";
            dataGridView1.Columns[4].HeaderText = "Телефон";
            dataGridView1.Columns[5].HeaderText = "Группа";
            dataGridView1.Columns[6].HeaderText = "Дата рождения";
            dataGridView1.Columns[7].HeaderText = "Дата начала учебы";
            dataGridView1.Columns[8].HeaderText = "Дата конца учебы";
            dataGridView1.Columns[9].HeaderText = "Степендия";
            dataGridView1.Columns[10].HeaderText = "Логин";
            dataGridView1.Columns[11].HeaderText = "Пароль";
            dataGridView1.Columns[12].HeaderText = "Пол";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                guna2TextBox7.ReadOnly = false;
                guna2TextBox7.FillColor = Color.White;
                guna2TextBox7.Text = "";
            }
            else {
                guna2TextBox7.ReadOnly = true;
                guna2TextBox7.FillColor = Color.Silver;
                guna2TextBox7.Text = "0";
            }
        }

        private void btn_editProfile_Click(object sender, EventArgs e)
        {
            string zapros = "INSERT INTO `student` (`id`, `fullname`, `address`, `email`, `number`, `course`, `salary`, `dob`, `ed`, `dl`, `login`, `password`, `sex`) VALUES" +
                "(NULL, '" + guna2TextBox1.Text + "', '" + guna2TextBox2.Text + "', '" + guna2TextBox4.Text + "', '" + guna2TextBox3.Text + "', '" + comboBox1.Text + "'," +
                " '" + guna2TextBox7.Text + "', '" + guna2DateTimePicker1.Value.ToString("yyyy-MM-dd") + "', '" + guna2DateTimePicker2.Value.ToString("yyyy-MM-dd") + "'," +
                " '" + guna2DateTimePicker3.Value.ToString("yyyy-MM-dd") + "', '" + guna2TextBox10.Text + "','"+ guna2TextBox11.Text +"','"+comboBox2.Text+"')";
            Database.DB db = new Database.DB();
            db.openConnection();
            MySqlCommand comm = new MySqlCommand(zapros, db.getConnection());
            comm.ExecuteNonQuery();
            db.closeConnection();

            UpdateDatabase();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
            int d = Convert.ToInt32(dataGridView1.Rows[id].Cells[0].Value);
            string zapros = "UPDATE `student` SET `fullname` = @fullname, `address` = @address, `email` = @email, `number` = @number, `course` = @course, `salary` = @salary, `dob` = @dob, `ed` = @ed, `dl` = @dl, `login` = @login, `password` = @password, `sex` = @sex WHERE `id` = @id";

            Database.DB db = new Database.DB();
            db.openConnection();

            MySqlCommand comm = new MySqlCommand(zapros, db.getConnection());
            comm.Parameters.AddWithValue("@fullname", guna2TextBox1.Text);
            comm.Parameters.AddWithValue("@address", guna2TextBox2.Text);
            comm.Parameters.AddWithValue("@email", guna2TextBox4.Text);
            comm.Parameters.AddWithValue("@number", guna2TextBox3.Text);
            comm.Parameters.AddWithValue("@course", comboBox1.Text);
            comm.Parameters.AddWithValue("@salary", guna2TextBox7.Text);
            comm.Parameters.AddWithValue("@dob", guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"));
            comm.Parameters.AddWithValue("@ed", guna2DateTimePicker2.Value.ToString("yyyy-MM-dd"));
            comm.Parameters.AddWithValue("@dl", guna2DateTimePicker3.Value.ToString("yyyy-MM-dd"));
            comm.Parameters.AddWithValue("@login", guna2TextBox10.Text);
            comm.Parameters.AddWithValue("@password", guna2TextBox11.Text);
            comm.Parameters.AddWithValue("@sex", comboBox2.Text);
            comm.Parameters.AddWithValue("@id", d);

            comm.ExecuteNonQuery();
            db.closeConnection();

            UpdateDatabase();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            guna2TextBox1.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox2.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox3.Text = Convert.ToString(dataGridView1[4, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox4.Text = Convert.ToString(dataGridView1[3, dataGridView1.CurrentRow.Index].Value);
            comboBox1.Text = Convert.ToString(dataGridView1[5, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox7.Text = Convert.ToString(dataGridView1[9, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox10.Text = Convert.ToString(dataGridView1[10, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox11.Text = Convert.ToString(dataGridView1[11, dataGridView1.CurrentRow.Index].Value);
            guna2DateTimePicker1.Text = Convert.ToString(dataGridView1[6, dataGridView1.CurrentRow.Index].Value);
            guna2DateTimePicker2.Text = Convert.ToString(dataGridView1[7, dataGridView1.CurrentRow.Index].Value);
            guna2DateTimePicker3.Text = Convert.ToString(dataGridView1[8, dataGridView1.CurrentRow.Index].Value);
            comboBox2.Text = Convert.ToString(dataGridView1[11, dataGridView1.CurrentRow.Index].Value);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
            int d = Convert.ToInt32(dataGridView1.Rows[id].Cells[0].Value);
            string del = "DELETE FROM `student` WHERE `id` = " + d + "";
            Database.DB db = new Database.DB();
            db.openConnection();
            MySqlCommand comm = new MySqlCommand(del, db.getConnection());
            comm.ExecuteNonQuery();
            db.closeConnection();

            UpdateDatabase();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            comboBox1.Text = "";
            guna2TextBox7.Text = "0";
            guna2TextBox10.Text = "";
            guna2TextBox11.Text = "";
            guna2DateTimePicker1.Text = "";
            guna2DateTimePicker2.Text = "";
            guna2DateTimePicker3.Text = "";
            comboBox2.Text = "";
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox7.Text != "0")
            {
                checkBox1.Checked = true;
            }
            else 
            {
                checkBox1.Checked = false;
            }
        }

        private void guna2TextBox12_VisibleChanged(object sender, EventArgs e)
        {

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
                        saveFileDialog.FileName = "student.xlsx";

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
