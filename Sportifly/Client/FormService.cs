using Guna.UI2.WinForms;
using Sportifly.Classes;
using Sportifly.Model;
using Sportifly.Repository;
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

namespace Sportifly.User
{
    public partial class FormService : Form
    {
        private int SelectedServiceId { get; set; }
        private ServiceRepository ServiceRepository { get; set; } = new ServiceRepository();
        private DataBase DataBase { get; set; } = new DataBase();

        public FormService()
        {
            InitializeComponent();
        }

        private void ServicesCoach_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            LoadDataGrid();
            DataGridColumnVisible();
        }

        private void LoadDataGrid()
        {
            guna2DataGridView1.DataSource = ServiceRepository.GetServiceAll();
        }

        private void DataGridColumnVisible()
        {
            guna2DataGridView1.Columns["ID Услуги"].Visible = false;
            guna2DataGridView1.Columns["Сотрудник"].Visible = false;
            guna2DataGridView1.Columns["Описание"].Visible = false;
            guna2DataGridView1.Columns["Цена"].Visible = false;

            guna2DataGridView1.Columns["Номер телефона"].Visible = false;
            guna2DataGridView1.Columns["Почта"].Visible = false;
            guna2DataGridView1.Columns["Направление"].Visible = false;
            guna2DataGridView1.Columns["Опыт работы"].Visible = false;
            guna2DataGridView1.Columns["Фотография"].Visible = false;

            guna2DataGridView1.Columns["Фамилия"].Visible = false;
            guna2DataGridView1.Columns["Имя"].Visible = false;
            guna2DataGridView1.Columns["Отчество"].Visible = false;
            guna2DataGridView1.Columns["Пользователь"].Visible = false;
            guna2DataGridView1.Columns["Почта"].Visible = false;

            guna2DataGridView1.Columns["Логин аккаунта"].Visible = false;
            guna2DataGridView1.Columns["ID Тренера"].Visible = false;
            guna2DataGridView1.Columns["Тренер"].Visible = false;
            guna2DataGridView1.Columns["Фотография1"].Visible = false;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
            label1.Text = selectedRow.Cells["Название"].Value.ToString();
            label16.Text = selectedRow.Cells["Описание"].Value.ToString();
            label17.Text = selectedRow.Cells["Цена"].Value.ToString();
            label18.Text = selectedRow.Cells["Сотрудник"].Value.ToString();
            SelectedServiceId = (int)selectedRow.Cells["ID Услуги"].Value;
            panel1.Visible = true;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
            label13.Text = selectedRow.Cells["Номер телефона"].Value.ToString();
            label11.Text = selectedRow.Cells["Почта"].Value.ToString();
            label7.Text = selectedRow.Cells["Направление"].Value.ToString();
            label8.Text = selectedRow.Cells["Опыт работы"].Value.ToString();
            byte[] photoBytes = selectedRow.Cells["Фотография"].Value as byte[];
            if (photoBytes != null)
            {
                using (MemoryStream ms = new MemoryStream(photoBytes))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else
            {
                pictureBox1.Image = null;
            }
            panel2.Visible = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (DataBase.Execute("INSERT INTO [История записи на тренировки] (Клиент, Услуга, [Дата тренировки], [Статус тренировки])" +
                $"  VALUES ({PersonalData.PersonId}, {SelectedServiceId}, GETDATE(), 1);") != null)
            {
                MessageBox.Show("Вы успешно записались на тренировку!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
