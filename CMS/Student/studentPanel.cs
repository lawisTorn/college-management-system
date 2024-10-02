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
using CMS.Teacher;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;

namespace CMS.Student
{
    public partial class studentPanel : Form
    {
        public studentPanel()
        {
            InitializeComponent();
        }

        private void studentPanel_Load(object sender, EventArgs e)
        {
            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet dataSet = new DataSet();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student` WHERE `id` = @l", db.getConnection());
            command.Parameters.Add("@l", MySqlDbType.Text).Value = label1.Text;
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            label2.Text = Convert.ToString(dataGridView1[1, 0].Value);

            btn_profile_Click(sender, e);
            btn_profile.Checked = true;
        }

        private void tabControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void btn_profile_Click(object sender, EventArgs e)
        {

            string dob = Convert.ToString(dataGridView1[6, 0].Value);
            string ed = Convert.ToString(dataGridView1[7, 0].Value);
            string dl = Convert.ToString(dataGridView1[8, 0].Value);

            studentProfile sP = new studentProfile();
            tabControl(sP);
            sP.label20.Text = Convert.ToString(dataGridView1[1, 0].Value);
            sP.label18.Text = dob.Length >= 7 ? dob.Substring(0, dob.Length - 7) : dob;
            sP.label16.Text = Convert.ToString(dataGridView1[4, 0].Value);
            sP.label17.Text = Convert.ToString(dataGridView1[2, 0].Value);
            sP.label15.Text = Convert.ToString(dataGridView1[3, 0].Value);
            sP.label14.Text = ed.Length >= 7 ? ed.Substring(0, ed.Length - 7) : ed;
            sP.label11.Text = dl.Length >= 7 ? dl.Substring(0, dl.Length - 7) : dl;
            sP.label12.Text = Convert.ToString(dataGridView1[9, 0].Value) + " тг.";
            sP.label22.Text = Convert.ToString(dataGridView1[5, 0].Value);
            sP.label13.Text = Convert.ToString(dataGridView1[10, 0].Value);
            sP.label24.Text = label1.Text;

            try
            {
                if (dataGridView1[13, 0].Value != DBNull.Value)
                {
                    byte[] imageBytes = (byte[])dataGridView1[13, 0].Value;

                    if (imageBytes.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            sP.pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        sP.pictureBox1.Image = null; // or set to a default image
                    }
                }
                else
                {
                    sP.pictureBox1.Image = null; // or set to a default image
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
                sP.pictureBox1.Image = null; // or set to a default image
            }

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            
            studentGrade sG = new studentGrade();
            Database.DB db = new Database.DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `student` WHERE `id` = @id", db.getConnection());
            cmd.Parameters.Add("@id", MySqlDbType.Text).Value = label1.Text;
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
            sG.dataGridView2.DataSource = dt;

            tabControl(sG);
        }

        private void btn_student_Click(object sender, EventArgs e)
        {
            studentStudent sStudent = new studentStudent();
            Database.DB db = new Database.DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            MySqlCommand cmd = new MySqlCommand("SELECT `course` FROM `student` WHERE `id` = @id", db.getConnection());
            cmd.Parameters.Add("@id", MySqlDbType.Text).Value = label1.Text;
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
            sStudent.dataGridView2.DataSource = dt;



            tabControl(sStudent);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            studentTeacher studentTeacher = new studentTeacher();
            tabControl(studentTeacher);
        }

        private void studentPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
