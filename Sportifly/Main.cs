using Guna.UI2.WinForms;
using Sportifly.Admin;
using Sportifly.Classes;
using Sportifly.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sportifly
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

        }
        private Form acriveForm = null;
        private void openForm(Form childForm)

        {
            if (acriveForm != null)
                acriveForm.Close();
            acriveForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel1.Controls.Add(childForm);
            panel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void CheckRole(int role)
        {
            switch (role)
            {
                case 1: // Клиент
                        // Удаляем или скрываем метки, специфичные для клиента
                    label5.Dispose();
                    label4.Dispose();
                    // Загружаем форму приветствия для клиента
                    openForm(new Welcome());
                    break;

                case 2: // Тренер
                        // Удаляем или скрываем метки, специфичные для тренера
                    label5.Dispose();
                    // Загружаем форму приветствия для клиента
                    openForm(new Welcome());
                    break;

                case 3: // Администратор
                        // Удаляем или скрываем метки, специфичные для администратора
                    label1.Dispose();
                    // Загружаем форму приветствия для клиента
                    openForm(new Welcome());
                    break;

                default:
                    MessageBox.Show("Роль некорректна!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
            AdjustLabelPositions();
        }

        private void AdjustLabelPositions()
        {
            // Определим ширину изображения справа, чтобы не заезжать за него
            int imageWidth = 190; // Замените на фактическую ширину изображения
            int padding = 10; // Отступы между метками
            var labels = panel2.Controls.OfType<Label>().Where(label => label.Visible).ToList();

            if (labels.Count == 0)
                return;

            // Общая доступная ширина для меток, с учётом отступов
            int availableWidth = panel2.Width - imageWidth - padding * 2;

            // Определяем начальную позицию для меток по горизонтали
            int startX = padding;

            // Вычисляем равномерное расстояние между метками по горизонтали
            int spacing = availableWidth / (labels.Count + 1);

            // Находим максимальную высоту среди меток
            int maxHeight = labels.Max(label => label.Height);

            // Позиция по вертикали (все метки будут на одной высоте)
            int verticalPosition = (panel2.Height / 2) - (maxHeight / 2);

            // Применяем максимальную высоту ко всем меткам
            foreach (var label in labels)
            {
                label.Height = maxHeight; // Устанавливаем всем лейблам одинаковую высоту
            }

            // Располагаем все метки по горизонтали и выравниваем их по вертикали
            for (int i = 0; i < labels.Count; i++)
            {
                // Устанавливаем горизонтальную позицию для каждой метки
                labels[i].Location = new Point(startX + (spacing * (i + 1)) - (labels[i].Width / 2), verticalPosition);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            CheckRole(Convert.ToInt32(PersonalData.Role));
            openForm(new Welcome());
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            var LKForm = new LK();
            openForm(new LK());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            var ServicesCoachForm = new ServicesCoach();
            openForm(new ServicesCoach());
        }

        private void label5_Click(object sender, EventArgs e)
        {
            var ReportsReceiptsForm = new ReportsReceipts();
            openForm(new ReportsReceipts());
        }

        private void label4_Click(object sender, EventArgs e)
        {

            var AllUserssForm = new AllUsers();
            openForm(new AllUsers());
        }

        private void label1_Click(object sender, EventArgs e)
        {
            var TrainingSessionsForm = new TrainingSessions();
            openForm(new TrainingSessions());
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            openForm(new Main());
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
