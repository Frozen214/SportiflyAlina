using Guna.UI2.WinForms;
using Sportifly.Classes;
using Sportifly.Model;
using System;
using System.Windows.Forms;

namespace Sportifly.User
{
    public partial class TechSupport : Form
    {
        DataBase db;
        private SupportModel Support = new SupportModel();
        public TechSupport()
        {
            db = new DataBase();
            InitializeComponent();
        }

        private void TechSupport_Load(object sender, EventArgs e)
        {
            Support.ClientId = PersonalData.UserId;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем значение роли в зависимости от выбранного текста в ComboBox
                int categoryId = 0;

                if (guna2ComboBox1.Text == "Проблема в приложении")
                {
                    categoryId = 1;
                }
                else if (guna2ComboBox1.Text == "Нет подходящего предложения")
                {
                    categoryId = 2;
                }
                else if (guna2ComboBox1.Text == "Проблема работы с тренером")
                {
                    categoryId = 3;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите категорию обращения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var query = " insert into [Тех.поддержка] (Сообщение, Категория, Пользователь)" +
                       $" values ('{guna2TextBox1.Text}', {categoryId}, {PersonalData.UserId})";
                if (string.IsNullOrEmpty(guna2TextBox1.Text))
                {
                    MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (db.Execute(query) != null)
                {
                    SmsSender smsSender = new SmsSender();
                    smsSender.SendMessage($"Новое общение от {PersonalData.Email}", $"Новое общение от {PersonalData.Email}, Суть обращения:{guna2TextBox1.Text}. Вопрос по: {guna2ComboBox1.Text}");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в отпрвке. Ошибка {ex}", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }

    }
}

