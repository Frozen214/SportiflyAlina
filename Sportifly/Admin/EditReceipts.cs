using Guna.UI2.WinForms;
using Sportifly.Classes;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sportifly.Admin
{
    public partial class EditReceipts : Form
    {
        DB db;
        private string currId; // Внутреннее поле для CurrId

        public EditReceipts()
        {
            db = new DB();
            InitializeComponent();
        }
       

        public string CurrId
        {
            get { return currId; }
            set { currId = value; }
        }

        public string PaymentMethod
        {
            set { guna2ComboBox1.SelectedItem = value; }
        }

        public string StatusPayment
        {
            set { guna2ComboBox2.SelectedItem = value; }
        }

        private void EditReceipts_Load(object sender, EventArgs e)
        {
            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            // Заполнение комбобокса для выбора способа оплаты
            string paymentMethodQuery = "SELECT [ID способа], [Наименование] FROM [Способ оплаты]";
            db.FillComboBox(paymentMethodQuery, "Наименование", guna2ComboBox1);

            guna2ComboBox1.DisplayMember = "Наименование";
            guna2ComboBox1.ValueMember = "ID способа";

            // Заполнение комбобокса для выбора статуса платежа
            string paymentStatusQuery = "SELECT [ID статуса], [Название статуса] FROM [Статусы платежей]";
            db.FillComboBox(paymentStatusQuery, "Название статуса", guna2ComboBox2);

            guna2ComboBox2.DisplayMember = "Название статуса";
            guna2ComboBox2.ValueMember = "ID статуса";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
                try
                {
                    int paymentId = 0;

                    // Определение способа оплаты по тексту
                    if (guna2ComboBox1.Text == "Оплата картой лично")
                    {
                        paymentId = 1;
                    }
                    else if (guna2ComboBox1.Text == "Оплата наличными лично")
                    {
                        paymentId = 2;
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите способ оплаты.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int statusId = 0;

                    // Определение статуса платежа по тексту
                    if (guna2ComboBox2.Text == "Ожидается оплата")
                    {
                        statusId = 1;
                    }
                    else if (guna2ComboBox2.Text == "Оплачено")
                    {
                        statusId = 2;
                    }
                    else if (guna2ComboBox2.Text == "Отменено")
                    {
                        statusId = 3;
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите статус платежа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string query = @"
            UPDATE Платежи
            SET [Статус платежа] = @StatusId,
                [Способ оплаты] = @PaymentMethodId
            WHERE [ID платежа] = @IdPayment";

                    using (SqlConnection myCon = new SqlConnection(db.StringConn))
                    {
                        myCon.Open(); // Открываем соединение

                        using (SqlCommand cmd = new SqlCommand(query, myCon))
                        {
                            cmd.Parameters.AddWithValue("@StatusId", statusId); // Устанавливаем статус
                            cmd.Parameters.AddWithValue("@PaymentMethodId", paymentId); // Устанавливаем способ оплаты
                            cmd.Parameters.AddWithValue("@IdPayment", int.Parse(currId)); // Используем ID платежа

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Статус платежа обновлен!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Не удалось обновить статус платежа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            

        }
    }
}
