using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Sportifly.Classes;

namespace Sportifly.ForgotPass
{
    public partial class Recovery : Form
    {

        public string code;
        public string randomcode;
        public static string to;
        public static string InputMail;
        public string login = Recovery.InputMail;

        DB db;

        public Recovery()
        {

            db = new DB();
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Настройки SMTP-сервера Mail.ru
            string smtpServer = "smtp.mail.ru";
            int smtpPort = 587;
            string smtpUsername = "sportifly31@mail.ru";
            string smtpPassword = "QfyLEzd4sK0rqmm5Kxtf";

            to = guna2TextBox1.Text.Trim();

            if (string.IsNullOrEmpty(to))
            {
                MessageBox.Show("Введите e-mail");
                return;
            }

            Random rand = new Random();
            randomcode = rand.Next(100000, 999999).ToString();
            InputMail = to;

            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(smtpUsername);
                    mailMessage.To.Add(to);
                    mailMessage.Subject = "Код подтверждения Sportifly";
                    mailMessage.Body = $"Используйте этот код для сброса пароля вашей учетной записи:\nВаш код: {randomcode}";

                    try
                    {
                        smtpClient.Send(mailMessage);
                        MessageBox.Show("Код успешно отправлен");
                      
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка отправки кода: {ex.Message}");
                    }
                }
            }
        }

        private void guna2TextBox1_Enter(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "Введите e-mail")
            {
                guna2TextBox1.Text = "";
                guna2TextBox1.ForeColor = Color.FromArgb(28, 27, 94);
            }
        }

        private void guna2TextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text))
            {
                guna2TextBox1.Text = "Введите e-mail";
                guna2TextBox1.ForeColor = Color.Gray;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            Login authorize = new Login();
            authorize.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text.Trim() == randomcode)
            {
                Recovery.to = guna2TextBox1.Text;
                NewPass newPassForm = new NewPass();
                this.Hide();
                newPassForm.Show();
            }
            else
            {
                MessageBox.Show("Неправильный код");
            }
        }

        private void guna2TextBox2_Enter(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "Введите код подтверждения")
            {
                guna2TextBox2.Text = "";
                guna2TextBox2.ForeColor = Color.FromArgb(28, 27, 94);
            }
        }

        private void guna2TextBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox2.Text))
            {
                guna2TextBox2.Text = "Введите код подтверждения";
                guna2TextBox2.ForeColor = Color.Gray;
            }
        }

    }
}
    
    
