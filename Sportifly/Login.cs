using Sportifly.Classes;
using Sportifly.ForgotPass;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sportifly
{
    public partial class Login : Form
    {
        private PersonalData PI {  get; set; } = new PersonalData();
        private bool isVisiblePassword = false;
        private static string BasePath = BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../");
        private Image ViewImage = Image.FromFile($@"{BasePath}макет\view.png");
        private Image HideImage = Image.FromFile($@"{BasePath}макет\hide.png");

        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            var formRegistration = new Registr();
            formRegistration.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (isVisiblePassword == true)
            {
                isVisiblePassword = false;
                pictureBox2.Image = HideImage;
                guna2TextBox2.UseSystemPasswordChar = true;
            }
            else
            {
                isVisiblePassword = true;
                pictureBox2.Image = ViewImage;
                guna2TextBox2.UseSystemPasswordChar = false;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var login = guna2TextBox1.Text;
            var password = guna2TextBox2.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните поля!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!PI.SetPersonalData(login, password))
            {
                MessageBox.Show("Данные не корректные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"{PersonalData.Name} " + $"{PersonalData.FatherName}, Добро пожаловать!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var mainForm = new Main();
            mainForm.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            FormRecoveryAccess formRecovery = new FormRecoveryAccess();
            formRecovery.Show();
            this.Hide();
        }
    }
}
