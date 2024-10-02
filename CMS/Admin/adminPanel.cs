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
using MySql.Data.MySqlClient;

namespace CMS.Admin
{
    public partial class adminPanel : Form
    {
        public adminPanel()
        {
            InitializeComponent();
        }

        private void adminPanel_Load(object sender, EventArgs e)
        {
            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet dataSet = new DataSet();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `admin` WHERE `id` = @l", db.getConnection());
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
            adminProfile aPr = new adminProfile();
            tabControl(aPr);

            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `admin` WHERE `id` = @l", db.getConnection());
            command.Parameters.Add("@l", MySqlDbType.Text).Value = label1.Text;
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            aPr.dataGridView1.DataSource = dt;
            DataTable dt_ = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            MySqlCommand command1 = new MySqlCommand("SELECT COUNT(*) FROM `teacher`", db.getConnection());
            adapter.SelectCommand = command1;
            adapter.Fill(dt_);
            aPr.dataGridView2.DataSource = dt_;

            MySqlCommand command2 = new MySqlCommand("SELECT COUNT(*) FROM `student`", db.getConnection());
            adapter.SelectCommand = command2;
            adapter.Fill(dt1);
            aPr.dataGridView3.DataSource = dt1;

            MySqlCommand command3 = new MySqlCommand("SELECT COUNT(*) FROM `subjects`", db.getConnection());
            adapter.SelectCommand = command3;
            adapter.Fill(dt2);
            aPr.dataGridView4.DataSource = dt2;

            MySqlCommand command4 = new MySqlCommand("SELECT COUNT(*) FROM `courses`", db.getConnection());
            adapter.SelectCommand = command4;
            adapter.Fill(dt3);
            aPr.dataGridView5.DataSource = dt3;

            aPr.label17.Text = Convert.ToString(aPr.dataGridView2[0, 0].Value);
            aPr.label18.Text = Convert.ToString(aPr.dataGridView3[0, 0].Value);
            aPr.label19.Text = Convert.ToString(aPr.dataGridView4[0, 0].Value);
            aPr.label20.Text = Convert.ToString(aPr.dataGridView5[0, 0].Value);
            string dob = Convert.ToString(aPr.dataGridView1[5, 0].Value);
            aPr.label15.Text = label1.Text;
            aPr.label12.Text = Convert.ToString(aPr.dataGridView1[1, 0].Value);
            aPr.label10.Text = dob.Length >= 7 ? dob.Substring(0, dob.Length - 7) : dob; ;
            aPr.label9.Text = Convert.ToString(aPr.dataGridView1[2, 0].Value);
            aPr.label8.Text = Convert.ToString(aPr.dataGridView1[3, 0].Value);
            aPr.label7.Text = Convert.ToString(aPr.dataGridView1[4, 0].Value);
            aPr.label13.Text = Convert.ToString(aPr.dataGridView1[6, 0].Value);

            try
            {
                if (aPr.dataGridView1[8, 0].Value != DBNull.Value)
                {
                    byte[] imageBytes = (byte[])aPr.dataGridView1[8, 0].Value;

                    if (imageBytes.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            aPr.pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        aPr.pictureBox1.Image = null; // or set to a default image
                    }
                }
                else
                {
                    aPr.pictureBox1.Image = null; // or set to a default image
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
                aPr.pictureBox1.Image = null; // or set to a default image
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            adminTeacher aT = new adminTeacher();
            tabControl(aT);
        }

        private void btn_student_Click(object sender, EventArgs e)
        {
            adminStudent aS = new adminStudent();
            tabControl(aS);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            adminCourse aC = new adminCourse();
            tabControl(aC);
        }

        private void btn_subject_Click(object sender, EventArgs e)
        {
            adminSubject aSu = new adminSubject();
            tabControl(aSu);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void adminPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
