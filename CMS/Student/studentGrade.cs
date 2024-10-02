using CMS.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace CMS.Student
{
    public partial class studentGrade : UserControl
    {
        public studentGrade()
        {
            InitializeComponent();
        }

        private void studentGrade_Load(object sender, EventArgs e)
        {
            UpdateDatabase();

            checkBox1.Checked = false;
            

            comboboxdb();
            comboBox1.Items.Add("Нет");

            for (int i = 0; i < dataGridView3.RowCount - 1; i++)
            {
                comboBox1.Items.Add(dataGridView3.Rows[i].Cells[1].Value);
            }

            comboBox1.SelectedIndex = 0;

            comboBox2.Items.Add("Нет");
            comboBox2.Items.Add("A");
            comboBox2.Items.Add("B");
            comboBox2.Items.Add("C");
            comboBox2.Items.Add("D");
            comboBox2.Items.Add("E");

            comboBox2.SelectedIndex = 0;
        }

        private void UpdateDatabase() {
            Database.DB db = new Database.DB();
            string name_s = Convert.ToString(dataGridView2[1, 0].Value);
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd_grade = new MySqlCommand("SELECT * FROM `grade` WHERE `name_student` = '" + name_s + "'", db.getConnection());
            adapter.SelectCommand = cmd_grade;
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


        }

        private void comboboxdb() {
            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd_sub = new MySqlCommand("SELECT * FROM `subjects`", db.getConnection());
            adapter.SelectCommand = cmd_sub;
            adapter.Fill(dt);
            dataGridView3.DataSource = dt;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) {
                guna2DateTimePicker2.Enabled = true;
                guna2DateTimePicker1.Enabled = true;
                guna2DateTimePicker3.Enabled = false;
            }
            else if (checkBox1.Checked == false) {
                guna2DateTimePicker2.Enabled = false;
                guna2DateTimePicker1.Enabled = false;
                guna2DateTimePicker3.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string zapros = "";
            string name_s = Convert.ToString(dataGridView2[1, 0].Value);

            Database.DB db = new Database.DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dataTable = new DataTable();

            if (checkBox1.Checked == false)
            {
                if (comboBox1.SelectedIndex != 0)
                {
                    if (comboBox2.SelectedIndex != 0)
                    {
                        zapros = "SELECT * FROM `grade` WHERE `name_student` = '" + name_s + "' AND `rating` = '" + comboBox2.Text + "' AND `subject` = '" + comboBox1.Text + "' AND `date` = '" + Convert.ToString(guna2DateTimePicker3.Value.ToString("yyyy-MM-dd")) + "'";

                    }
                    else if (comboBox2.SelectedIndex == 0) {
                        zapros = "SELECT * FROM `grade` WHERE `name_student` = '" + name_s + "' AND `subject` = '" + comboBox1.Text + "' AND `date` = '" + Convert.ToString(guna2DateTimePicker3.Value.ToString("yyyy-MM-dd")) + "'";
                    }
                }
                else if (comboBox1.SelectedIndex == 0) {
                    if (comboBox2.SelectedIndex != 0)
                    {
                        zapros = "SELECT * FROM `grade` WHERE `name_student` = '" + name_s + "' AND `rating` = '" + comboBox2.Text + "' AND `date` = '" + Convert.ToString(guna2DateTimePicker3.Value.ToString("yyyy-MM-dd")) + "'";
                    }
                    else if (comboBox2.SelectedIndex == 0) {
                        zapros = "SELECT * FROM `grade` WHERE `name_student` = '" + name_s + "' AND `date` = '" + Convert.ToString(guna2DateTimePicker3.Value.ToString("yyyy-MM-dd")) + "'";
                    }
                }
            }
            else if (checkBox1.Checked == true)
            {
                if (guna2DateTimePicker2.Value > guna2DateTimePicker1.Value) {
                    MessageBox.Show("Не правильно выставлен период!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (guna2DateTimePicker2.Value < guna2DateTimePicker1.Value) {
                    if (comboBox1.SelectedIndex != 0) {
                        if (comboBox2.SelectedIndex != 0)
                        {
                            zapros = "SELECT * FROM `grade` WHERE `name_student` = '" + name_s + "' AND `rating` = '" + comboBox2.Text + "' AND `subject` = '" + comboBox1.Text + "' AND `date` BETWEEN '" + Convert.ToString(guna2DateTimePicker2.Value.ToString("yyyy-MM-dd")) + "' AND '" + Convert.ToString(guna2DateTimePicker1.Value.ToString("yyyy-MM-dd")) + "'";
                        }
                        else if (comboBox2.SelectedIndex == 0) {
                            zapros = "SELECT * FROM `grade` WHERE `name_student` = '" + name_s + "' AND `subject` = '" + comboBox1.Text + "' AND `date` BETWEEN '" + Convert.ToString(guna2DateTimePicker2.Value.ToString("yyyy-MM-dd")) + "' AND '" + Convert.ToString(guna2DateTimePicker1.Value.ToString("yyyy-MM-dd")) + "'";
                        }
                    }
                    else if (comboBox1.SelectedIndex == 0) {
                        if (comboBox2.SelectedIndex != 0 )
                        {
                            zapros = "SELECT * FROM `grade` WHERE `name_student` = '" + name_s + "' AND `rating` = '" + comboBox2.Text + "' AND `date` BETWEEN '" + Convert.ToString(guna2DateTimePicker2.Value.ToString("yyyy-MM-dd")) + "' AND '" + Convert.ToString(guna2DateTimePicker1.Value.ToString("yyyy-MM-dd")) + "'";

                        }
                        else if (comboBox2.SelectedIndex == 0)
                        {
                            zapros = "SELECT * FROM `grade` WHERE `name_student` = '" + name_s + "' AND `date` BETWEEN '" + Convert.ToString(guna2DateTimePicker2.Value.ToString("yyyy-MM-dd")) + "' AND '" + Convert.ToString(guna2DateTimePicker1.Value.ToString("yyyy-MM-dd")) + "'";
                        }
                    }
                }
            }
            MySqlCommand cmd = new MySqlCommand(zapros,db.getConnection());
            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            string name_s = Convert.ToString(dataGridView1[2, dataGridView1.CurrentRow.Index].Value);
            string sub = Convert.ToString(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
            string date = Convert.ToString(dataGridView1[6, dataGridView1.CurrentRow.Index].Value);
            string name_t = Convert.ToString(dataGridView1[4, dataGridView1.CurrentRow.Index].Value);
            string rating = Convert.ToString(dataGridView1[3, dataGridView1.CurrentRow.Index].Value);
            string course = Convert.ToString(dataGridView1[5, dataGridView1.CurrentRow.Index].Value);

            label19.Text = name_s;
            label18.Text = sub;
            label15.Text = name_t;
            label17.Text = rating;
            label16.Text = course;
            label13.Text = date.Length >= 7 ? date.Substring(0, date.Length - 7) : date;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            UpdateDatabase();
            checkBox1.Checked = false;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            guna2DateTimePicker1.Value = DateTime.Today;
            guna2DateTimePicker2.Value = DateTime.Today;
            guna2DateTimePicker3.Value = DateTime.Today;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
