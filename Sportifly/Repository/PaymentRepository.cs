using Sportifly.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sportifly.Repository
{
    public class PaymentRepository
    {
        private DataBase db { get; set; } = new DataBase();

        public bool UpdateStatus(int paymentId, int statusId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(db.ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("dbo.UpdatePaymentStatus", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@PaymentId", paymentId);
                command.Parameters.AddWithValue("@StatusId", statusId);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Ошибка базы данных: {sqlEx.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataTable GetPaymentStatus()
        {
            try
            {
                var connection = new SqlConnection(db.ConnectionString);
                connection.Open();

                var command = new SqlCommand("SELECT * FROM dbo.[Статусы платежей]", connection)
                {
                    CommandType = CommandType.Text
                };

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при получении записей из Статусы платежей.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }
    }
}
