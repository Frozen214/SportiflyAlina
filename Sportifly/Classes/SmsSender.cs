using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sportifly.Classes
{
    public class SmsSender
    {
        public void SendMessage(string ThemText, string BodyText)
        {
            string smtpServer = "smtp.mail.ru";
            int smtpPort = 587;
            string smtpUsername = "feesheep-bot@mail.ru"; // почта, с которой отправляется сообщение
            string smtpPassword = "2jC279daALafZBviqhfY";//пароль приложения (от почты)


            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {

                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(smtpUsername);
                    mailMessage.To.Add("alya.kamyshnikova.99@bk.ru"); // адрес получателя
                    mailMessage.Subject = ThemText;
                    mailMessage.Body = BodyText;

                    try
                    {
                        // Отправляем сообщение
                        smtpClient.Send(mailMessage);
                        MessageBox.Show("Обращение успешно отправлено!\r\nСкоро с Вами свяжутся.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка отправки сообщения: {ex.Message}", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    
    }
}
