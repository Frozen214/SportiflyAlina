using Sportifly.Repository;
using System;
using System.Windows.Forms;

namespace Sportifly.Admin
{
    public partial class EditReceipts : Form
    {
        private PaymentRepository PaymentRepository { get; set; } = new PaymentRepository();
        private ReportsReceipts ReportsReceipts { get; set; }
        private int StatusId { get; set; }
        private int PaymentId { get; set; }

        public EditReceipts(int statusId, int paymentId, ReportsReceipts reportsReceipts)
        {
            InitializeComponent();
            StatusId = statusId;
            PaymentId = paymentId;
            ReportsReceipts = reportsReceipts;
        }

        private void EditReceipts_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            guna2ComboBox2.SelectedValue = StatusId;
        }

        private void LoadCombobox()
        {
            guna2ComboBox2.DataSource = PaymentRepository.GetPaymentStatus();
            guna2ComboBox2.DisplayMember = "Название статуса";
            guna2ComboBox2.ValueMember = "ID статуса";
            guna2ComboBox2.SelectedIndex = 0;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (PaymentRepository.UpdateStatus(PaymentId, StatusId))
            {
                MessageBox.Show("Статус платежа успешно изменен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReportsReceipts.LoadDataGrid();
                this.Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
