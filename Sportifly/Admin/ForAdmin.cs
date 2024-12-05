using Guna.UI2.WinForms;
using Sportifly.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sportifly
{
    public partial class ForAdmin : Form
    {
        DB db;
        Hashing getHash;

        public ForAdmin()
        {
            db = new DB();
            getHash = new Hashing();
            InitializeComponent();
        }

        private void ForAdmin_Load(object sender, EventArgs e)
        {
            FillComboBoxes();
        }

        /// <summary>
        /// Заполнение комбобоксов данными из базы данных.
        /// </summary>
        private void FillComboBoxes()
        {// Заполняем ComboBox1 ролями
            string roleQuery = "SELECT [ID роли], [Название роли] FROM Роли";
            db.FillComboBox(roleQuery, "Название роли", guna2ComboBox1);
            guna2ComboBox1.DisplayMember = "Название роли"; // Название роли для отображения
            guna2ComboBox1.ValueMember = "ID роли";          // ID роли как внутреннее значение

            // Заполнение остальных комбобоксов логинами пользователей
            string usersQuery = "SELECT Логин FROM Пользователи WHERE Логин <> '' AND Логин IS NOT NULL";
            db.FillComboBox(usersQuery, "Логин", guna2ComboBox3);
            db.FillComboBox(usersQuery, "Логин", guna2ComboBox4);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем значение роли в зависимости от выбранного текста в ComboBox
                int roleId = 0;

                if (guna2ComboBox1.Text == "Клиент")
                {
                    roleId = 1;
                }
                else if (guna2ComboBox1.Text == "Тренер")
                {
                    roleId = 2;
                }
                else if (guna2ComboBox1.Text == "Администратор")
                {
                    roleId = 3;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите корректную роль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Создаем словарь параметров для хранимой процедуры
                var parameters = new Dictionary<string, object>
    {
        { "@TypeOperation", (short)2 }, // Приведение к типу smallint
        { "@Login", guna2TextBox7.Text },
        { "@Password", getHash.Hash(guna2TextBox6.Text) },
        { "@Role", roleId }
    };

                // Формируем запрос для добавления новой учетной записи
                string query = @"
        INSERT INTO Пользователи (Логин, [Хэш пароль], Роль)
        VALUES (@Login, @Password, @Role)";

                // Выполняем запрос через ExecuteNonQuery
                int result = db.ExecuteNonQuery(query, parameters);

                if (result > 0)
                {
                    FillComboBoxes(); // Обновляем данные в ComboBox
                    MessageBox.Show("Учетная запись успешно добавлена!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Не удалось добавить учетную запись. Проверьте введенные данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var query = $"delete Пользователи where Логин='{guna2ComboBox3.SelectedItem}'";
            if (db.Execute(query) != null)
            {
                FillComboBoxes();
                MessageBox.Show("Пользователь успешно удален!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text))
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var query = $"update Пользователи set  [Хэш пароль]='{getHash.Hash(guna2TextBox1.Text)}' where Логин='{guna2ComboBox4.SelectedItem}'";
            if (db.Execute(query) != null)
            {
                MessageBox.Show("Данные успешно обновлены!!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox3.SelectedItem != null)
            {
                // Формируем запрос, используя представление
                string query = $@"
            SELECT Роль
            FROM vw_Users
            WHERE Логин = '{guna2ComboBox3.SelectedItem}'";

                // Получаем значение роли и присваиваем его в label10
                label10.Text = db.GetSignleValue(query, "Роль") ?? "Роль не найдена";
            }
            else
            {
                MessageBox.Show("Выберите пользователя из списка!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
