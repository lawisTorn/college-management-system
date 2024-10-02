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
    public partial class adminSubject : UserControl
    {
        public adminSubject()
        {
            InitializeComponent();
        }

        private void adminSubject_Load(object sender, EventArgs e)
        {
            UpdateDataBase();
            
        }

        private void UpdateDataBase() 
        {
            Database.DB db = new Database.DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet dataSet = new DataSet();
            MySqlCommand command = new MySqlCommand("SELECT `id`, `name` FROM `subjects`", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Код";
            dataGridView1.Columns[1].HeaderText = "Название предмета";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btn_editProfile_Click(object sender, EventArgs e)
        {
            Database.DB db = new Database.DB();
            string addSub = "INSERT INTO `subjects` (`id`, `name`) VALUES (NULL, '"+guna2TextBox2.Text+"')";
            MySqlCommand cmd = new MySqlCommand(addSub, db.getConnection());
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
            string addSub = "UPDATE subjects SET `name` = @name WHERE `id` = @id";
            MySqlCommand cmd = new MySqlCommand(addSub, db.getConnection());
            cmd.Parameters.AddWithValue("@name", guna2TextBox2.Text);
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
            string del = "DELETE FROM `subjects` WHERE `id` = " + d + "";
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
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            guna2TextBox1.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
            guna2TextBox2.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
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
