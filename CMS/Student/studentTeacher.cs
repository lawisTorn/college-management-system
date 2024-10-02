using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMS.Student
{
    public partial class studentTeacher : UserControl
    {
        public studentTeacher()
        {
            InitializeComponent();
        }

        private void studentTeacher_Load(object sender, EventArgs e)
        {
            Updatedatabase();
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
        private void Updatedatabase() {
            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `id`, `fullname`, `email`, `number`, `subject`, `course`, `dob` FROM `teacher`", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Код";
            dataGridView1.Columns[1].HeaderText = "Полное ФИО";
            dataGridView1.Columns[2].HeaderText = "Почта";
            dataGridView1.Columns[3].HeaderText = "Телефон";
            dataGridView1.Columns[4].HeaderText = "Предмет";
            dataGridView1.Columns[5].HeaderText = "Группа";
            dataGridView1.Columns[6].HeaderText = "Дата рождения";
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            string email = Convert.ToString(dataGridView1[2, dataGridView1.CurrentRow.Index].Value);
            string subject = Convert.ToString(dataGridView1[4, dataGridView1.CurrentRow.Index].Value);
            string dob = Convert.ToString(dataGridView1[6, dataGridView1.CurrentRow.Index].Value);

            label19.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
            label17.Text = Convert.ToString(dataGridView1[3, dataGridView1.CurrentRow.Index].Value);
            label16.Text = email;
            label15.Text = dob.Length >= 7 ? dob.Substring(0, dob.Length - 7) : dob;
            label14.Text = subject;
            label13.Text = Convert.ToString(dataGridView1[5, dataGridView1.CurrentRow.Index].Value);
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

            string zapros = "";

            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            if (comboBox1.SelectedIndex != 0)
            {
                if (comboBox2.SelectedIndex != 0) {
                    zapros = "SELECT `id`, `fullname`, `email`, `number`, `subject`, `course`, `dob` FROM `teacher` WHERE `course` = '" + comboBox2.Text + "' AND `subject` = '" + comboBox1.Text + "'";
                }
                else if (comboBox2.SelectedIndex == 0) {
                    zapros = "SELECT `id`, `fullname`, `email`, `number`, `subject`, `course`, `dob` FROM `teacher` WHERE `subject` = '" + comboBox1.Text + "'";
                }
            }
            else if (comboBox1.SelectedIndex == 0) 
            {
                if (comboBox2.SelectedIndex != 0)
                {
                    zapros = "SELECT `id`, `fullname`, `email`, `number`, `subject`, `course`, `dob` FROM `teacher` WHERE `course` = '" + comboBox2.Text + "'";
                }
                else if (comboBox2.SelectedIndex == 0) {
                    zapros = "SELECT `id`, `fullname`, `email`, `number`, `subject`, `course`, `dob` FROM `teacher`";
                }
            }

            MySqlCommand cmd = new MySqlCommand(zapros, db.getConnection());
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Updatedatabase();
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
        }

        /* private string TruncateText(string text, int maxLength)
         {
             if (text.Length > maxLength)
             {
                 return text.Substring(0, maxLength) + "...";
             }
             return text;
         }*/
    }
}
