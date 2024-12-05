using Guna.UI2.WinForms;
using Microsoft.Office.Interop.Word;
using Sportifly.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;
using Document = Microsoft.Office.Interop.Word.Document;

namespace Sportifly.Admin
{
    public partial class ReportsReceipts : Form
    {
        DB db;

        private string selectedNumber;
        private string selectedServiceName;
        private decimal selectedPrice;
        private string selectedPaymentMethod;
        private string selectedPaymentStatus;
        private DateTime selectedPaymentDate;

        public ReportsReceipts()
        {
            InitializeComponent();
            db = new DB();
        }



        private void guna2Button1_Click(object sender, EventArgs e )
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку","Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
                DataGridViewRow row = guna2DataGridView1.SelectedRows[0];

                selectedNumber = row.Cells["ID платежа"].Value?.ToString() ?? "";
                selectedServiceName = row.Cells["Название услуги"].Value?.ToString() ?? "";
                selectedPrice = Convert.ToDecimal(row.Cells["Цена услуги"].Value ?? 0);
                selectedPaymentMethod = row.Cells["Способ оплаты"].Value?.ToString() ?? "";
                selectedPaymentStatus = row.Cells["Статус платежа"].Value?.ToString() ?? "";
                selectedPaymentDate = Convert.ToDateTime(row.Cells["Дата платежа"].Value ?? DateTime.Now);
            

            string inputFilePath = $@"{System.Windows.Forms.Application.StartupPath}\Receipt\Example.docx";
            string outputFilePath = $@"{System.Windows.Forms.Application.StartupPath}\Receipt\Receipt {DateTime.Now:dd-MM-yyyy HH-mm-ss}.pdf";


            var replacements = new Dictionary<string, string>
    {
        { "<Номер>", selectedNumber },
        { "<Услуга>", selectedServiceName },
        { "<Сумма>", $"{selectedPrice:F2} руб." },
        { "<СтатусПлатежа>", selectedPaymentStatus },
        { "<СпособОплаты>", selectedPaymentMethod },
        { "<Дата>", selectedPaymentDate.ToString("dd.MM.yyyy HH:mm:ss") }
    };

            if (ReplaceTags(inputFilePath, outputFilePath, replacements))
            {
                MessageBox.Show("Чек успешно сформирован.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public static bool ReplaceTags(string inputFilePath, string outputFilePath, System.Collections.Generic.Dictionary<string, string> replacements)
        {
            Application wordApp = new Application();
            try
            {
                Document doc = wordApp.Documents.Open(inputFilePath, ReadOnly: true);
                Document newDoc = wordApp.Documents.Add();
                doc.Content.Copy();
                newDoc.Content.Paste();

                foreach (var replacement in replacements)
                {
                    newDoc.Content.Find.Execute(FindText: replacement.Key, ReplaceWith: replacement.Value, Replace: WdReplace.wdReplaceAll);
                }

                newDoc.SaveAs2(outputFilePath, WdSaveFormat.wdFormatPDF);
                newDoc.Close(SaveChanges: false);
                doc.Close(SaveChanges: false);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при формировании чека: " + ex.Message, "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                wordApp.Quit();
            }
        }

        private void ReportsReceipts_Load(object sender, EventArgs e)
        {
            FillData();
        }
        private void FillData()
        {
            var queryAllFeedback = "select * from [vw_Платежи]";
            db.ReturnData(queryAllFeedback, guna2DataGridView1);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
           
            if (guna2DataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Текущая строка не выбрана!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            EditReceipts editReceipts = new EditReceipts();
            editReceipts.Show();
            string idPayment = guna2DataGridView1.CurrentRow.Cells["ID платежа"].Value.ToString();
            string budget = guna2DataGridView1.CurrentRow.Cells["Цена услуги"].Value.ToString();
            string paymentMethod = guna2DataGridView1.CurrentRow.Cells["Способ оплаты"].Value.ToString();
            string statusPayment = guna2DataGridView1.CurrentRow.Cells["Статус платежа"].Value.ToString();
        }
    }
}
