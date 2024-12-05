using Guna.Charts.WinForms;
using Sportifly.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Sportifly.ForgotPass
{
    public partial class NewPass : Form
    {
        DB db;

        public NewPass()
        {
            db = new DB();
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Hashing GH = new Hashing();
            string password = guna2TextBox1.Text.Trim();
            string confirmationPass = guna2TextBox2.Text.Trim();

            if (confirmationPass == password)
            {
                try
                {
                    string hashedPassword = GH.Hash(password);
                    string commandText = @"
                    UPDATE Пользователи 
                    SET [Хэш пароль] = @password 
                    WHERE Почта = @userEmail";

                    var parameters = new Dictionary<string, object>
                {
                    { "@password", hashedPassword },
                    { "@userEmail", Recovery.userEmail } // Используем сохраненную почту
                };

                    int rowsAffected = db.ExecuteNonQuery(commandText, parameters);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Пароль успешно обновлен");
                        Login form = new Login();
                        form.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось обновить пароль. Пожалуйста, попробуйте снова.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают! Попробуйте снова.");
            }
        }
    }

}
