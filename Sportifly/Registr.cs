using Sportifly.Classes;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sportifly
{
    public partial class Registr : Form
    {
        private DataBase db { get; set; } = new DataBase();
        private Hashing getHash { get; set; } = new Hashing();

        private bool isVisiblePassword = false;
        private static string BasePath = BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../");
        private UserValidator PasswordValidation { get; set; } = new UserValidator();
        private Image ViewImage { get; set; } = Image.FromFile($@"{BasePath}макет\view.png");
        private Image HideImage { get; set; } = Image.FromFile($@"{BasePath}макет\hide.png");

        public Registr()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
            this.Hide();
        }

        public void guna2Button1_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                MessageBox.Show("Заполните все поля!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (guna2TextBox5.Text != guna2TextBox6.Text)
            {
                MessageBox.Show("Пароли не совпадают!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] parts = guna2TextBox1.Text.Split(' ');
            if (parts.Length != 3)
            {
                MessageBox.Show("ФИО некорректно. Введите ФИО в формате \"Иванов Иван Иванович\" ", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!PasswordValidation.EmailValidation(guna2TextBox6.Text))
            {
                return;
            }

            if (!PasswordValidation.PasswordValidation(guna2TextBox6.Text))
            {
                return;
            }

            var query = $@"exec dbo.RegistrationUser 
                                @Name = '{parts[1]}'
                                ,@FatherName = '{parts[2]}'
                                ,@Surname = '{parts[0]}'
                                ,@NumTel='{guna2TextBox2.Text}'
                                ,@Email='{guna2TextBox3.Text}'
                                ,@Login='{guna2TextBox4.Text}'
                                ,@HashPassword='{getHash.Hash(guna2TextBox5.Text)}'";

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

        public bool ValidateForm()
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text) ||
                string.IsNullOrEmpty(guna2TextBox2.Text) ||
                string.IsNullOrEmpty(guna2TextBox3.Text) ||
                string.IsNullOrEmpty(guna2TextBox4.Text) ||
                string.IsNullOrEmpty(guna2TextBox5.Text) ||
                string.IsNullOrEmpty(guna2TextBox6.Text))
            {
                return false;
            }

            return true;
        }
    }
}
