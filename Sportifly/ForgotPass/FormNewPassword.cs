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
    public partial class FormNewPassword : Form
    {
        DataBase db;

        public FormNewPassword()
        {
            db = new DataBase();
            InitializeComponent();
        }

            private void guna2Button1_Click(object sender, EventArgs e)
            {
                Hashing GH = new Hashing();
                string password = guna2TextBox1.Text;
                string confirmationPass = guna2TextBox2.Text;

                if (confirmationPass == password)
                {
                    try
                    {
                        string hashedPassword = GH.Hash(password);

                        // SQL-запрос для обновления пароля
                        string commandText = @"
                UPDATE Пользователи 
                SET [Хэш пароль] = @password 
                WHERE [ID пользователя] IN (
                    SELECT [Пользователь]
                    FROM Клиенты 
                    WHERE [Почта] = @email
                    UNION
                    SELECT [Пользователь]
                    FROM Сотрудники 
                    WHERE [Почта] = @email
                )";

                        // Создаем параметры для защиты от SQL-инъекций
                        var parameters = new Dictionary<string, object>
            {
                { "@password", hashedPassword },
                { "@email", FormRecoveryAccess.userEmail }
            };

                        // Выполняем запрос
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
                            MessageBox.Show("Не удалось обновить пароль. Убедитесь, что почта верна.");
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
