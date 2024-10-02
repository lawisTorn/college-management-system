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
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using TheArtOfDevHtmlRenderer.Adapters;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using OfficeOpenXml;
using System.IO;

namespace CMS.Teacher
{
    public partial class teacherStudent : UserControl
    {
        public teacherStudent()
        {
            InitializeComponent();
        }

        private void teacherStudent_Load(object sender, EventArgs e)
        {
            UpdateDatabase();
            comboBox1.Items.Add("Нет");
            comboBox1.SelectedIndex = 0;


            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                comboBox1.Items.Add(dataGridView2.Rows[i].Cells[0].Value);
            }

            comboBox2.Items.Add("Нет");
            comboBox2.Items.Add("М");
            comboBox2.Items.Add("Ж");

            comboBox2.SelectedIndex = 0;


        }
        private void UpdateDatabase() {
            Database.DB db = new Database.DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT `id`, `fullname`, `address`, `number`, `email`, `course`, `dob`, `sex` FROM `student`", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Код";
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Адрес";
            dataGridView1.Columns[3].HeaderText = "Телефон";
            dataGridView1.Columns[4].HeaderText = "Почта";
            dataGridView1.Columns[5].HeaderText = "Группа";
            dataGridView1.Columns[6].HeaderText = "Дата рождения";
            dataGridView1.Columns[7].HeaderText = "Пол";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataTable dt2 = new DataTable();
            MySqlCommand command2 = new MySqlCommand("SELECT `name` FROM `courses`", db.getConnection());
            adapter.SelectCommand = command2;
            adapter.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.DB db = new Database.DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            string zapros_4 = "SELECT `id`, `fullname`, `address`, `number`, `email`, `course`, `dob`, `sex` FROM `student` WHERE `course` = '" + Convert.ToString(comboBox1.Text) + "'";
            string zapros_5 = "SELECT `id`, `fullname`, `address`, `number`, `email`, `course`, `dob`, `sex` FROM `student` WHERE `course` = '" + Convert.ToString(comboBox1.Text) + "' AND `sex` = '"+Convert.ToString(comboBox2.Text)+"'";

            if (comboBox1.SelectedIndex != 0) {
                if (comboBox2.SelectedIndex != 0)
                {
                    MySqlCommand command1 = new MySqlCommand(zapros_5, db.getConnection());
                    adapter.SelectCommand = command1;
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                else if (comboBox2.SelectedIndex == 0) {
                    MySqlCommand command1 = new MySqlCommand(zapros_4, db.getConnection());
                    adapter.SelectCommand = command1;
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            else if (comboBox1.SelectedIndex == 0) {
                UpdateDatabase();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            string email = Convert.ToString(dataGridView1[4, dataGridView1.CurrentRow.Index].Value);
            string dob = Convert.ToString(dataGridView1[6, dataGridView1.CurrentRow.Index].Value);


            label19.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
            label18.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentRow.Index].Value);
            label17.Text = Convert.ToString(dataGridView1[3, dataGridView1.CurrentRow.Index].Value);
            label16.Text = TruncateText(email, 23);
            label15.Text = dob.Length >= 7 ? dob.Substring(0, dob.Length - 7) : dob;
            label13.Text = Convert.ToString(dataGridView1[5, dataGridView1.CurrentRow.Index].Value);
            label11.Text = Convert.ToString(dataGridView1[7, dataGridView1.CurrentRow.Index].Value);
        }

        private string TruncateText(string text, int maxLength)
        {
            if (text.Length > maxLength)
            {
                return text.Substring(0, maxLength) + "...";
            }
            return text;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.DB db = new Database.DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            string zapros_4 = "SELECT `id`, `fullname`, `address`, `number`, `email`, `course`, `dob`, `sex` FROM `student` WHERE `sex` = '" + Convert.ToString(comboBox2.Text) + "'";
            string zapros_5 = "SELECT `id`, `fullname`, `address`, `number`, `email`, `course`, `dob`, `sex` FROM `student` WHERE `course` = '" + Convert.ToString(comboBox1.Text) + "' AND `sex` = '" + Convert.ToString(comboBox2.Text) + "'";

            if (comboBox2.SelectedIndex != 0)
            {
                if (comboBox1.SelectedIndex != 0)
                {
                    MySqlCommand command1 = new MySqlCommand(zapros_5, db.getConnection());
                    adapter.SelectCommand = command1;
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                else if (comboBox1.SelectedIndex == 0)
                {
                    MySqlCommand command1 = new MySqlCommand(zapros_4, db.getConnection());
                    adapter.SelectCommand = command1;
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            else if (comboBox1.SelectedIndex == 0)
            {
                UpdateDatabase();
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
