using System;
using System.Windows.Forms;

namespace Sportifly.Model
{
    public class PaymentDataModel
    {
        public string Number { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }

        public PaymentDataModel(DataGridViewRow row)
        {
            Number = row.Cells["ID платежа"].Value?.ToString() ?? "";
            ServiceName = row.Cells["Название услуги"].Value?.ToString() ?? "";
            Price = Convert.ToDecimal(row.Cells["Цена услуги"].Value ?? 0);
            PaymentMethod = row.Cells["Способ оплаты"].Value?.ToString() ?? "";
            PaymentStatus = row.Cells["Статус платежа"].Value?.ToString() ?? "";
            PaymentDate = Convert.ToDateTime(row.Cells["Дата платежа"].Value ?? DateTime.Now);
        }
    }
}
