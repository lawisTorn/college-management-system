using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CMS.Student
{
    public partial class studentStudent : UserControl
    {
        public studentStudent()
        {
            InitializeComponent();
        }

        private void studentStudent_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Нет");
            comboBox1.Items.Add("М");
            comboBox1.Items.Add("Ж");

            comboBox1.SelectedIndex = 0;

            string coursedb = Convert.ToString(dataGridView2[0, 0].Value);

            Database.DB db = new Database.DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand("SELECT `id`, `fullname`, `number`, `email`, `course`, `dob`, `sex` FROM `student` WHERE `course` = @id", db.getConnection());
            cmd.Parameters.Add("@id", MySqlDbType.Text).Value = coursedb;
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Код";
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Телефон";
            dataGridView1.Columns[3].HeaderText = "Почта";
            dataGridView1.Columns[4].HeaderText = "Группа";
            dataGridView1.Columns[5].HeaderText = "Дата рождения";
            dataGridView1.Columns[6].HeaderText = "Пол";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                guna2DateTimePicker1.Enabled = true;
                guna2DateTimePicker2.Enabled = true;
            }
            else if (checkBox1.Checked == false) {
                guna2DateTimePicker1.Enabled = false;
                guna2DateTimePicker2.Enabled = false;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            string zapros = "";
            string coursedb = Convert.ToString(dataGridView2[0, 0].Value);

            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            if (checkBox1.Checked == false) {
                if (comboBox1.SelectedIndex != 0) {
                    zapros = "SELECT `id`, `fullname`, `number`, `email`, `course`, `dob`, `sex` FROM `student` WHERE `course` = '"+coursedb+"' AND `sex` = '"+comboBox1.Text+"'";
                }
            }
            else if (checkBox1.Checked == true) {
                if (guna2DateTimePicker1.Value > guna2DateTimePicker2.Value) {
                    MessageBox.Show("Не правильно выставлен период!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (guna2DateTimePicker1.Value < guna2DateTimePicker2.Value) {
                    if (comboBox1.SelectedIndex != 0)
                    {
                        zapros = "SELECT `id`, `fullname`, `number`, `email`, `course`, `dob`, `sex` FROM `student` WHERE `course` = '" + coursedb + "' AND `sex` = '" + comboBox1.Text + "' AND `dob` BETWEEN '" + guna2DateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + guna2DateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
                    }
                    else if (comboBox1.SelectedIndex == 0) {
                        zapros = "SELECT `id`, `fullname`, `number`, `email`, `course`, `dob`, `sex` FROM `student` WHERE `course` = '" + coursedb + "' AND `dob` BETWEEN '" + Convert.ToString(guna2DateTimePicker1.Value.ToString("yyyy-MM-dd")) + "' AND '" + Convert.ToString(guna2DateTimePicker2.Value.ToString("yyyy-MM-dd")) + "'";
                    }
                }
            }

            MySqlCommand cmd = new MySqlCommand(zapros,db.getConnection());
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

            string coursedb = Convert.ToString(dataGridView2[0, 0].Value);

            Database.DB db = new Database.DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand("SELECT `id`, `fullname`, `number`, `email`, `course`, `dob`, `sex` FROM `student` WHERE `course` = @id", db.getConnection());
            cmd.Parameters.Add("@id", MySqlDbType.Text).Value = coursedb;
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Код";
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Телефон";
            dataGridView1.Columns[3].HeaderText = "Почта";
            dataGridView1.Columns[4].HeaderText = "Группа";
            dataGridView1.Columns[5].HeaderText = "Дата рождения";
            dataGridView1.Columns[6].HeaderText = "Пол";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            checkBox1.Checked = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            string email = Convert.ToString(dataGridView1[3, dataGridView1.CurrentRow.Index].Value);
            string dob = Convert.ToString(dataGridView1[5, dataGridView1.CurrentRow.Index].Value);


            label19.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
            label17.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentRow.Index].Value);
            label16.Text = TruncateText(email, 23);
            label15.Text = dob.Length >= 7 ? dob.Substring(0, dob.Length - 7) : dob;
            label13.Text = Convert.ToString(dataGridView1[4, dataGridView1.CurrentRow.Index].Value);
            label11.Text = Convert.ToString(dataGridView1[6, dataGridView1.CurrentRow.Index].Value);
        }
        private string TruncateText(string text, int maxLength)
        {
            if (text.Length > maxLength)
            {
                return text.Substring(0, maxLength) + "...";
            }
            return text;
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
    }
}
