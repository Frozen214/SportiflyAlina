using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sportifly.Classes
{
    public class DataBaseAdmin
    {
        private DB DB { get; set; }

        public DataBaseAdmin()
        {
            DB = new DB();
        }

        public DataTable ExecuteQuery(string sqlQuery)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = DB.ReturnData(sqlQuery, null); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения запроса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }
    }
}

