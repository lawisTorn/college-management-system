﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CMS.Admin
{
    public partial class adminEditProfile : Form
    {
        public adminEditProfile()
        {
            InitializeComponent();
        }

        private void btn_editProfile_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "" && guna2TextBox3.Text != "")
            {
                Database.DB db = new Database.DB();
                DataTable dt = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                if (btn_editProfile.Text == "Сменить пароль")
                {
                    if (guna2TextBox2.Text == guna2TextBox3.Text) 
                    { 
                        string pass = guna2TextBox1.Text;
                        string selectP = "SELECT `password` FROM `admin` WHERE `password` = @p AND `id` = @id";
                        MySqlCommand command1 = new MySqlCommand(selectP, db.getConnection());
                        command1.Parameters.Add("@p", MySqlDbType.Text).Value = pass;
                        command1.Parameters.Add("@id", MySqlDbType.Text).Value = label5.Text;
                        adapter.SelectCommand = command1;
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            db.openConnection();
                            string editP = "UPDATE `admin` SET `password` = @p WHERE `id` = @id";
                            MySqlCommand command2 = new MySqlCommand(editP, db.getConnection());
                            command2.Parameters.Add("@p", MySqlDbType.Text).Value = guna2TextBox2.Text;
                            command2.Parameters.Add("@id", MySqlDbType.Text).Value = label5.Text;
                            command2.ExecuteNonQuery();
                            db.closeConnection();

                            MessageBox.Show("Вы успешно сменили пароль!");

                            this.Close();
                        }
                        else
                        {
                            label4.Text = "Неверный пароль!";
                        }
                    }

                }
                else if (btn_editProfile.Text == "Сменить логин")
                {
                    string log = guna2TextBox1.Text;
                    string pass = guna2TextBox3.Text;
                    string log_ = guna2TextBox2.Text;
                    string selectL = "SELECT `login` FROM `admin` WHERE `login` = @l AND `id` = @id";
                    MySqlCommand command1 = new MySqlCommand(selectL, db.getConnection());
                    command1.Parameters.Add("@l", MySqlDbType.Text).Value = log;
                    command1.Parameters.Add("@id", MySqlDbType.Text).Value = label5.Text;
                    adapter.SelectCommand = command1;
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        string editp_ = "SELECT `login` FROM `admin` WHERE `login` = @log";
                        MySqlCommand command = new MySqlCommand(editp_, db.getConnection());
                        command.Parameters.Add("@log", MySqlDbType.Text).Value = log_;
                        adapter.SelectCommand = command;
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 1)
                        {
                            label4.Text = "Логин занят!";
                            
                        }
                        else {
                            string selectP = "SELECT `password` FROM `admin` WHERE `password` = @p AND `id` = @id";
                            MySqlCommand command2 = new MySqlCommand(selectP, db.getConnection());
                            command2.Parameters.Add("@p", MySqlDbType.Text).Value = pass;
                            command2.Parameters.Add("@id", MySqlDbType.Text).Value = label5.Text;
                            adapter.SelectCommand = command2;
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                db.openConnection();
                                string editP = "UPDATE `admin` SET `login` = @l WHERE `id` = @id";
                                MySqlCommand command3 = new MySqlCommand(editP, db.getConnection());
                                command3.Parameters.Add("@l", MySqlDbType.Text).Value = guna2TextBox2.Text;
                                command3.Parameters.Add("@id", MySqlDbType.Text).Value = label5.Text;
                                command3.ExecuteNonQuery();
                                db.closeConnection();

                                MessageBox.Show("Вы успешно сменили логин!");

                                this.Close();
                            }
                            else
                            {
                                label4.Text = "Невеный пароль!";
                            }

                        }
                    }
                    else 
                    {
                        label4.Text = "Невеный логин!";
                    }
                }
            }
            else 
            {
                label4.Text = "Поля не заполнены!";
            }
        }

        private void adminEditProfile_Load(object sender, EventArgs e)
        {
           
        }
    }
}
