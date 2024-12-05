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

    public partial class FormRecoveryAccess : Form
    {
        public static string code;
        public static string randomcode;
        public static string InputMail;
        public static string userEmail; // Сохраняем почту для последующего использования

        DataBase db;

        public FormRecoveryAccess()
        {
            db = new DataBase();
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string smtpServer = "smtp.mail.ru";
            int smtpPort = 587;
            string smtpUsername = "sportifly31@mail.ru";
            string smtpPassword = "QfyLEzd4sK0rqmm5Kxtf";

            userEmail = guna2TextBox1.Text.Trim();

            if (string.IsNullOrEmpty(userEmail))
            {
                MessageBox.Show("Введите e-mail");
                return;
            }

            Random rand = new Random();
            randomcode = rand.Next(100000, 999999).ToString();
            InputMail = userEmail;

            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(smtpUsername);
                    mailMessage.To.Add(userEmail);
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text.Trim() == randomcode)
            {
                if (guna2TextBox2.Text.Trim() == randomcode)
                {
                    FormRecoveryAccess.userEmail = guna2TextBox1.Text;
                    FormNewPassword newPassForm = new FormNewPassword();
                    this.Hide();
                    newPassForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Неправильный код");
            }
        }
    }
}
    
    
