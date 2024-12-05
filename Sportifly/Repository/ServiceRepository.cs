using Sportifly.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sportifly.Repository
{
    public class ServiceRepository
    {
        private DataBase dataBase { get; set; } = new DataBase();

        /// <summary>
        /// Получает все записи из таблицы услуг.
        /// </summary>
        /// <returns>DataTable с записями из таблицы услуг.</returns>
        public DataTable GetServiceAll()
        {
            try
            {
                var connection = new SqlConnection(dataBase.ConnectionString);
                connection.Open();

                var command = new SqlCommand(@"SELECT с.*, у.*
                                          , с1.Фотография
                                        FROM Услуги у
                                          LEFT JOIN vw_Employee с ON у.Тренер = с.[ID тренера]
                                          INNER  JOIN Сотрудники с1 ON у.Тренер = с1.[ID тренера]
                                        ", connection);
                command.CommandType = CommandType.Text;

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при получении записей из таблицы Услуги.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }
    }
}
