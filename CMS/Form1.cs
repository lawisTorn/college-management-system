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

namespace CMS
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            string a = "";
            int index = comboBox1.SelectedIndex;
            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            try
            {
                db.getConnection().Open();
                switch (index)
                {
                    case 0:
                        a = "SELECT * FROM `admin` WHERE `login` = @l AND `password` = @p";
                        break;
                    case 1:
                        a = "SELECT * FROM `teacher` WHERE `login` = @l AND `password` = @p";
                        break;
                    case 2:
                        a = "SELECT * FROM `student` WHERE `login` = @l AND `password` = @p";
                        break;
                }
                MySqlCommand command = new MySqlCommand(a, db.getConnection());
                command.Parameters.Add("@l", MySqlDbType.Text).Value = login;
                command.Parameters.Add("@p", MySqlDbType.Text).Value = password;
                adapter.SelectCommand = command;
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    switch (index)
                    {
                        case 0:
                            Admin.adminPanel ap = new Admin.adminPanel();
                            ap.label1.Text = Convert.ToString(dataGridView1[0, 0].Value);
                            ap.Show();
                            this.Hide();
                            break;
                        case 1:
                            Teacher.teacherPanel tp = new Teacher.teacherPanel();
                            tp.label1.Text = Convert.ToString(dataGridView1[0, 0].Value);
                            tp.Show();
                            this.Hide();
                            break;
                        case 2:
                            Student.studentPanel sp = new Student.studentPanel();
                            sp.label1.Text = Convert.ToString(dataGridView1[0, 0].Value);
                            sp.Show();
                            this.Hide();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Неверные логин или пароль!");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message);
            }
            finally
            {
                db.getConnection().Close();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
