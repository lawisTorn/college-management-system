using CMS.Student;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace CMS.Teacher
{
    public partial class teacherProfile : UserControl
    {
        public teacherProfile()
        {
            InitializeComponent();
        }

        private void teacherProfile_Load(object sender, EventArgs e)
        {
            label19.Text = "Преподователь";
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            teacherEdit tE = new teacherEdit();
            tE.label5.Text = Convert.ToString(dataGridView1[0, 0].Value);
            tE.label1.Text = "Пароль";
            tE.label2.Text = "Новый пароль";
            tE.label3.Text = "Повторить пароль";
            tE.btn_editProfile.Text = "Сменить пароль";
            tE.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            teacherEdit tE = new teacherEdit();
            tE.label5.Text = Convert.ToString(dataGridView1[0, 0].Value);
            tE.label1.Text = "Логин";
            tE.label2.Text = "Новый логин";
            tE.label3.Text = "Пароль";
            tE.btn_editProfile.Text = "Сменить логин";
            tE.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Закрываем текущую форму, в которой находится adminProfile
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.Close();
            }

            // Открываем новую форму авторизации
            Form1 f1 = new Form1();
            f1.Show();
        }

       

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // Создание диалога выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите изображение";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            // Открытие диалога выбора файла
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Проверка файла на наличие изображения
                    Image selectedImage = Image.FromFile(openFileDialog.FileName);

                    // Загрузка выбранного изображения в PictureBox
                    pictureBox1.Image = selectedImage;

                    SaveImageToDatabase(openFileDialog.FileName);

                }
                catch (Exception ex)
                {
                    // Обработка ошибок и вывод сообщения
                    MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveImageToDatabase(string filePath)
        {

            Database.DB db = new Database.DB();
            byte[] imageData = File.ReadAllBytes(filePath);
            string id = label21.Text;

            try
            {
                db.openConnection();
                string query = "UPDATE `teacher` SET `img` = @ImageData WHERE `id` = @id";
                MySqlCommand cmd = new MySqlCommand(query, db.getConnection());
                cmd.Parameters.AddWithValue("@ImageData", imageData);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                db.closeConnection();
                MessageBox.Show("Изображение успешно сохранено в базу данных.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изображения в базу данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
