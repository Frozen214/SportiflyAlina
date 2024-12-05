using Guna.UI2.WinForms;
using Sportifly.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sportifly.Admin
{
    public partial class EditUsers : Form
    {
        public string CurrIDUser; // Идентификатор пользователя
        DB db; // Объект для работы с базой данных

        public EditUsers() // Конструктор формы
        {
            db = new DB();
            InitializeComponent();
        }

        public string LastName // Свойство для Фамилии
        {
            get { return guna2TextBox11.Text; }
            set { guna2TextBox11.Text = value; }
        }

        public string FirstName // Свойство для Имени
        {
            get { return guna2TextBox10.Text; }
            set { guna2TextBox10.Text = value; }
        }

        public string MiddleName // Свойство для Отчества
        {
            get { return guna2TextBox3.Text; }
            set { guna2TextBox3.Text = value; }
        }
        public string Email // Свойство для Почты
        {
            get { return guna2TextBox4.Text; }
            set { guna2TextBox4.Text = value; }
        }
        public string PhoneNumber // Свойство для Номера телефона
        {
            get { return guna2TextBox5.Text; }
            set { guna2TextBox5.Text = value; }
        }

        public string Role 
        {
            get { return guna2ComboBox1.SelectedItem?.ToString() ?? string.Empty; }
            set
            {
                if (guna2ComboBox1.Items.Contains(value))
                {
                    guna2ComboBox1.SelectedItem = value;
                }
            }
        }
        private void FillComboBoxes()
        {// Заполняем ComboBox1 ролями
            string roleQuery = "SELECT [ID роли], [Название роли] FROM Роли";
            db.FillComboBox(roleQuery, "Название роли", guna2ComboBox1);
            guna2ComboBox1.DisplayMember = "Название роли"; // Название роли для отображения
            guna2ComboBox1.ValueMember = "ID роли";          // ID роли как внутреннее значение

        }
        private void EditUsers_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
