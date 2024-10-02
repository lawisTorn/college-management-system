using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;

namespace CMS.Admin
{
    public partial class adminCourse : UserControl
    {
        public adminCourse()
        {
            InitializeComponent();
        }

        private void adminCourse_Load(object sender, EventArgs e)
        {
            UpdateDataBase();

            Database.DB db1 = new Database.DB();
            DataTable dt1 = new DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
            string gz = "SELECT `fullname` FROM `teacher`";
            MySqlCommand group = new MySqlCommand(gz, db1.getConnection());
            adapter1.SelectCommand = group;
            adapter1.Fill(dt1);
            dataGridView2.DataSource = dt1;

          
        }

        private void UpdateDataBase() 
        {
            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet dataSet = new DataSet();
            MySqlCommand command = new MySqlCommand("SELECT `id`, `name`, `speciality` FROM `courses`", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Код";
            dataGridView1.Columns[1].HeaderText = "Название группы";
            dataGridView1.Columns[2].HeaderText = "Специльность";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            guna2TextBox1.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox2.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox3.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentRow.Index].Value);

        }

        private void btn_editProfile_Click(object sender, EventArgs e)
        {
            Database.DB db = new Database.DB();
            string addCourse = "INSERT INTO `courses` (`id`, `name`, `speciality`) VALUES " +
                "(NULL, '"+guna2TextBox2.Text+"', '"+guna2TextBox3.Text+"')";
            MySqlCommand cmd = new MySqlCommand(addCourse, db.getConnection());
            db.openConnection();
            cmd.ExecuteNonQuery();
            db.closeConnection();

            UpdateDataBase();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
            int d = Convert.ToInt32(dataGridView1.Rows[id].Cells[0].Value);
            Database.DB db = new Database.DB();
            string updateCourse = "UPDATE `courses` SET `name` = @name, `speciality` = @speciality WHERE `id` = @id";
            MySqlCommand cmd = new MySqlCommand(updateCourse, db.getConnection());
            cmd.Parameters.AddWithValue("@name", guna2TextBox2.Text);
            cmd.Parameters.AddWithValue("@speciality", guna2TextBox3.Text);
            cmd.Parameters.AddWithValue("@id", d);

            db.openConnection();
            cmd.ExecuteNonQuery();
            db.closeConnection();

            UpdateDataBase();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
            int d = Convert.ToInt32(dataGridView1.Rows[id].Cells[0].Value);
            string del = "DELETE FROM `courses` WHERE `id` = " + d + "";
            Database.DB db = new Database.DB();
            db.openConnection();
            MySqlCommand comm = new MySqlCommand(del, db.getConnection());
            comm.ExecuteNonQuery();
            db.closeConnection();

            UpdateDataBase();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";

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
