using Guna.UI2.WinForms;
using Sportifly.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sportifly.User
{
    public partial class TrainingSessions : Form
    {
        DB db;
        public TrainingSessions()
        {
            db = new DB();
            InitializeComponent();
        }

        private void TrainingSessions_Load(object sender, EventArgs e)
        {
            FillData();
        }
        private void FillData()
        {
            var query = $"SELECT Клиент, Название, [Название статуса], [Дата тренировки] FROM vw_HistoryTraning WHERE [ID Клиента] = {PersonalData.IdUsers}";
            db.ReturnData(query, guna2DataGridView1);
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = guna2TextBox7.Text.Trim();

                string query = $@"
    SELECT *
    FROM vw_HistoryTraning
    WHERE 
        ([Клиент] LIKE '%{searchText}%' OR
        [Название] LIKE '%{searchText}%' OR
        [Название статуса] LIKE '%{searchText}%' OR
        [Дата тренировки] LIKE '%{searchText}%')
        AND [ID Клиента] = {PersonalData.IdUsers}";

                db.ReturnData(query, guna2DataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
