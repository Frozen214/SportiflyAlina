using Sportifly.Classes;
using Sportifly.Common;
using Sportifly.Model;
using System;
using System.Windows.Forms;

namespace Sportifly.Admin
{
    public partial class ReportsReceipts : Form
    {
        private DataBase db { get; set; } = new DataBase();

        public ReportsReceipts()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!IsSelecedRow())
            {
                return;
            }

            DataGridViewRow row = guna2DataGridView1.SelectedRows[0];
            var paymentData = new PaymentDataModel(row);

            string inputFilePath = $@"{Application.StartupPath}\Receipt\Example.docx";
            string outputFilePath = $@"{Application.StartupPath}\Receipt\Receipt {DateTime.Now:dd-MM-yyyy HH-mm-ss}.pdf";

            var receiptGenerator = new ReceiptGenerator();
            if (receiptGenerator.GenerateReceipt(paymentData, inputFilePath, outputFilePath))
            {
                MessageBox.Show("Чек успешно сформирован.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ReportsReceipts_Load(object sender, EventArgs e)
        {
            LoadDataGrid();
            SetVisibleColumn();
        }

        public void LoadDataGrid()
        {
            var queryAllFeedback = "select * from [vw_Платежи]";
            db.ReturnData(queryAllFeedback, guna2DataGridView1);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (!IsSelecedRow())
            {
                return;
            }

            var paymentId = (int)guna2DataGridView1.CurrentRow.Cells["ID платежа"].Value;
            var statusId = (int)guna2DataGridView1.CurrentRow.Cells["ID Статуса платежа"].Value;
            EditReceipts editReceipts = new EditReceipts(statusId, paymentId, this);
            editReceipts.ShowDialog();
        }

        private bool IsSelecedRow()
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Для данного действия выберите платеж!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void SetVisibleColumn()
        {
            guna2DataGridView1.Columns["ID Статуса платежа"].Visible = false;
            guna2DataGridView1.Columns["ID Платежа"].Visible = false;
        }
    }
}
