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
using CMS.Admin;
using MySql.Data.MySqlClient;

namespace CMS.Teacher
{
    public partial class teacherPanel : Form
    {
        public teacherPanel()
        {
            InitializeComponent();
        }
        private void teacherPanel_Load(object sender, EventArgs e)
        {
            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet dataSet = new DataSet();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `teacher` WHERE `id` = @l", db.getConnection());
            command.Parameters.Add("@l", MySqlDbType.Text).Value = label1.Text;
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            label2.Text = Convert.ToString(dataGridView1[1, 0].Value);

            btn_profile_Click(sender, e);
            btn_profile.Checked  = true;

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
            teacherProfile tPr = new teacherProfile();
            tabControl(tPr);

            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet dataSet = new DataSet();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `teacher` WHERE `id` = @l", db.getConnection());
            command.Parameters.Add("@l", MySqlDbType.Text).Value = label1.Text;
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            tPr.dataGridView1.DataSource = dt;

            string dob = Convert.ToString(tPr.dataGridView1[8, 0].Value);
            string ed = Convert.ToString(tPr.dataGridView1[9, 0].Value);
            tPr.label20.Text = Convert.ToString(tPr.dataGridView1[1, 0].Value);
            tPr.label18.Text = dob.Length >= 7 ? dob.Substring(0, dob.Length - 7) : dob;
            tPr.label16.Text = Convert.ToString(tPr.dataGridView1[4, 0].Value);
            tPr.label17.Text = Convert.ToString(tPr.dataGridView1[2, 0].Value);
            tPr.label15.Text = Convert.ToString(tPr.dataGridView1[3, 0].Value);
            tPr.label14.Text = ed.Length >= 7 ? ed.Substring(0, ed.Length - 7) : ed;
            tPr.label13.Text = Convert.ToString(tPr.dataGridView1[10, 0].Value);
            tPr.label12.Text = Convert.ToString(tPr.dataGridView1[6, 0].Value);
            tPr.label11.Text = Convert.ToString(tPr.dataGridView1[5, 0].Value);

            tPr.label21.Text = label1.Text;

            try
            {
                if (tPr.dataGridView1[12, 0].Value != DBNull.Value)
                {
                    byte[] imageBytes = (byte[])tPr.dataGridView1[12, 0].Value;

                    if (imageBytes.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            tPr.pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        tPr.pictureBox1.Image = null; 
                    }
                }
                else
                {
                    tPr.pictureBox1.Image = null; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
                tPr.pictureBox1.Image = null; 
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            teacherTeacher tPr = new teacherTeacher();
            tabControl(tPr);
        }

        private void btn_student_Click(object sender, EventArgs e)
        {
            teacherStudent tS = new teacherStudent();
            tabControl(tS);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            teacherGrade tG = new teacherGrade();
            tG.label1.Text = label2.Text;
            tabControl(tG);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void teacherPanel_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
