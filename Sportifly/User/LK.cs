using Sportifly.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sportifly.User
{
    public partial class LK : Form
    {
        DB db;
        Hashing getHash;
        public LK()
        {
            db = new DB();
            getHash = new Hashing();
            InitializeComponent();
        }

        private void LK_Load(object sender, EventArgs e)
        {
            SetData();
            LoadUserPhoto();
            var retriever = new ImageRetriever(db.StringConn);
            retriever.Retrieve(pictureBox1, Convert.ToInt32(PersonalData.IdUsers));
            guna2Panel1.Width = 0;
        }

        private void SetData()
        {
            guna2TextBox1.Text = PersonalData.Surname;
            guna2TextBox2.Text = PersonalData.Name;
            guna2TextBox3.Text = PersonalData.FatherName;
            guna2TextBox4.Text = PersonalData.Email;
            guna2TextBox5.Text = PersonalData.NumTel;
            label4.Text = $"{PersonalData.Surname} {PersonalData.Name} {PersonalData.FatherName}";
            label5.Text = PersonalData.Email;
            label6.Text = PersonalData.NumTel;
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text) || string.IsNullOrEmpty(guna2TextBox2.Text) || string.IsNullOrEmpty(guna2TextBox3.Text) || string.IsNullOrEmpty(guna2TextBox4.Text) || string.IsNullOrEmpty(guna2TextBox5.Text))
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var query = $"exec SetPersonalInfoUser @TypeOperation = 1, @Surname='{guna2TextBox1.Text}',@Name='{guna2TextBox2.Text}'," +
                $"@FatherName='{guna2TextBox3.Text}',@Email='{guna2TextBox4.Text}',@NumTel = '{guna2TextBox5.Text}',@Role='{PersonalData.Role}'," +
                $"@IdClient={PersonalData.IdEmployeeOrClient},@IdCoach={PersonalData.IdEmployeeOrClient}";
            if (db.Execute(query) != null)
            {
                MessageBox.Show("Персональная информация успешно обновлена", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox7.Text) || // Старый пароль
              string.IsNullOrEmpty(guna2TextBox8.Text) || // Новый пароль
              string.IsNullOrEmpty(guna2TextBox9.Text))   // Повтор нового пароля
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Проверка правильности старого пароля
            string oldPasswordHash = getHash.Hash(guna2TextBox7.Text);
            var checkOldPasswordQuery = $"SELECT COUNT(1) FROM Пользователи WHERE Логин = '{guna2TextBox6.Text}' AND Пароль = '{oldPasswordHash}'";
            int userExists = Convert.ToInt32(db.GetSignleValue(checkOldPasswordQuery, "COUNT(1)"));

            if (userExists == 0)
            {
                MessageBox.Show("Неверный текущий пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка совпадения новых паролей
            if (guna2TextBox8.Text != guna2TextBox9.Text)
            {
                MessageBox.Show("Новые пароли не совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Если старый пароль верный и новые пароли совпадают, обновляем данные
            var query = $"exec SetDataLK @Login = '{guna2TextBox6.Text}', @Password='{getHash.Hash(guna2TextBox8.Text)}', @IdUser = {PersonalData.IdUsers}";
            if (db.Execute(query) != null)
            {
                MessageBox.Show("Данные для входа успешно обновлены", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Не удалось обновить данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
            var uploader = new ImageUploader(db.StringConn);
            uploader.Upload(pictureBox1, Convert.ToInt32(PersonalData.IdUsers));
        }
        
        private IDisposable OpenFileDialog()
        {
            throw new NotImplementedException();
        }
        private void LoadUserPhoto()
        {
            // Проверяем, кто вошел в систему: клиент или тренер
            string tableName = PersonalData.Role == "Клиент" ? "Клиенты" : "Сотрудники";
            string query = $"SELECT Фотография FROM {tableName} WHERE Пользователь = @IdUsers";

            using (var connection = new SqlConnection(db.StringConn))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdUsers", PersonalData.IdEmployeeOrClient);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader["Фотография"] != DBNull.Value)
                        {
                            var imageData = (byte[])reader["Фотография"];
                            using (var memoryStream = new MemoryStream(imageData))
                            {
                                pictureBox1.Image = Image.FromStream(memoryStream);
                            }
                        }
                        else
                        {
                            // Установите изображение по умолчанию, если фото нет
                            pictureBox1.Image = null;
                        }
                    }
                }
            }
        }
       

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var uploader = new ImageUploader(db.StringConn);
            uploader.Upload(pictureBox1, Convert.ToInt32(PersonalData.IdUsers));
            MessageBox.Show("Фотография обновлена", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information );
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        bool isPanelOpen = true; // Переменная состояния панели (открыта или закрыта)

        private async Task AnimatePanelAsync()
        {
            if (isPanelOpen)
            {
                // Уменьшаем ширину панели с анимацией
                for (int i = 0; i < 10; i++)
                {
                    guna2Panel1.Width -= 55; // Уменьшаем ширину на 40
                    await Task.Delay(50);    // Задержка для плавности анимации
                }
                isPanelOpen = false; // Панель закрыта
            }
            else
            {
                // Увеличиваем ширину панели с анимацией
                for (int i = 0; i < 10; i++)
                {
                    guna2Panel1.Width += 55; // Увеличиваем ширину на 40
                    await Task.Delay(50);    // Задержка для плавности анимации
                }
                isPanelOpen = true; // Панель открыта
            }
        }
       

        private async void pictureBox2_Click(object sender, EventArgs e)
        {
            // Уменьшаем ширину панели с анимацией
            for (int i = 0; i < 10; i++)
            {
                guna2Panel1.Width -= 55; // Уменьшаем ширину на 40
                await Task.Delay(50);    // Задержка для плавности анимации
            }
            isPanelOpen = false; // Панель закрыта
        }
        private async void guna2Button3_Click(object sender, EventArgs e)
        {
            await AnimatePanelAsync();
        }
        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login formLogin = new Login();
            formLogin.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
