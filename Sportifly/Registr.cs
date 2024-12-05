using Sportifly.Classes;
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

namespace Sportifly
{
    public partial class Registr : Form
    {
        DB db;
        Hashing getHash;

        private bool isVisiblePassword = false;
        private static string BasePath = BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../");
        Validation PasswordValidation = new Validation();
        

        public Registr()
        {
            db = new DB();
            getHash = new Hashing();
            InitializeComponent();
        }

        Image ViewImage = Image.FromFile($@"{BasePath}макет\view.png");
        Image HideImage = Image.FromFile($@"{BasePath}макет\hide.png");

        private void label2_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
            this.Hide();
        }

        public void guna2Button1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(guna2TextBox1.Text) || string.IsNullOrEmpty(guna2TextBox2.Text) || string.IsNullOrEmpty(guna2TextBox3.Text) || string.IsNullOrEmpty(guna2TextBox4.Text) || string.IsNullOrEmpty(guna2TextBox5.Text))
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (guna2TextBox5.Text != guna2TextBox6.Text)
            {
                MessageBox.Show("Пароли не совпадают!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Разбиение ФИО
            string[] parts = guna2TextBox1.Text.Split(' ');
            if (parts.Length != 3)
            {
                MessageBox.Show("ФИО некорректно. Введите ФИО в формате \"Иванов Иван Иванович\" ", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!PasswordValidation.ValidationEmail(guna2TextBox6.Text))
            {
                return;
            } 
            if (!PasswordValidation.ValidationPass(guna2TextBox6.Text))
            {
                return;
            }

            var query = $"Exec RegistrationUser @Name = '{parts[1]}',@FatherName = '{parts[2]}',@Surname = '{parts[0]}',@NumTel='{guna2TextBox2.Text}'" +
                $" ,@Email='{guna2TextBox3.Text}',@Login='{guna2TextBox4.Text}',@HashPassword='{getHash.Hash(guna2TextBox5.Text)}'";

            if (db.Execute(query) != null)
            {
                MessageBox.Show("Регистрация прошла успешно!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Login formLogin = new Login();
                formLogin.Show();
                this.Hide();
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (isVisiblePassword == true)
            {
                isVisiblePassword = false;
                pictureBox2.Image = HideImage;
                guna2TextBox5.UseSystemPasswordChar = true;
            }
            else
            {
                isVisiblePassword = true;
                pictureBox2.Image = ViewImage;
                guna2TextBox5.UseSystemPasswordChar = false;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (isVisiblePassword == true)
            {
                isVisiblePassword = false;
                pictureBox2.Image = HideImage;
                guna2TextBox6.UseSystemPasswordChar = true;
            }
            else
            {
                isVisiblePassword = true;
                pictureBox2.Image = ViewImage;
                guna2TextBox6.UseSystemPasswordChar = false;
            }
        }

        private void Registr_Load(object sender, EventArgs e)
        {
            guna2TextBox6.UseSystemPasswordChar = true;
            guna2TextBox5.UseSystemPasswordChar = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public string ValidateForm()
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text) ||
                string.IsNullOrEmpty(guna2TextBox2.Text) ||
                string.IsNullOrEmpty(guna2TextBox3.Text) ||
                string.IsNullOrEmpty(guna2TextBox4.Text) ||
                string.IsNullOrEmpty(guna2TextBox5.Text) ||
                string.IsNullOrEmpty(guna2TextBox6.Text))
            {
                return "Заполните все поля!";
            }

            if (guna2TextBox5.Text != guna2TextBox6.Text)
            {
                return "Пароли не совпадают!";
            }

            // Другие проверки можно добавить здесь

            return null; // Ошибок нет
        }

    }
}
