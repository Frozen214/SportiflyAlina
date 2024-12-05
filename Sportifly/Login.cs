using Sportifly.Classes;
using Sportifly.ForgotPass;
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
    public partial class Login : Form
    {
        PersonalData PI;
        Hashing getHash;
        DB db;
        private bool isVisiblePassword = false;
        private static string BasePath = BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../");

        public Login()
        {
            getHash = new Hashing();
            db = new DB();
            PI = new PersonalData();
            InitializeComponent();
        }

        Image ViewImage = Image.FromFile($@"{BasePath}макет\view.png");
        Image HideImage = Image.FromFile($@"{BasePath}макет\hide.png");

     

        private void label2_Click(object sender, EventArgs e)
        {
            Registr Registr = new Registr();
            Registr.Show();
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
            //проверим пользователя на существование
            if (!PI.SetPersonalData(login, password))
            {
                MessageBox.Show("Данные не корректные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //откроем форму, если все проверки пройдены
            var mainForm = new Main();
            mainForm.Show();
            this.Hide();
            MessageBox.Show($"{PersonalData.Name} " + $"{PersonalData.FatherName}, Добро пожаловать!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Recovery Recovery = new Recovery();
            Recovery.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
