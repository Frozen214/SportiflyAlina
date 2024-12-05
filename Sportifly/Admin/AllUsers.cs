using Sportifly.Admin;
using Sportifly.Classes;
using Sportifly.Common;
using Sportifly.Infrastructure;
using Sportifly.Interface;
using Sportifly.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sportifly
{
    public partial class AllUsers : Form
    {
        DB db;

        private readonly IUserService _userservice;
        private DGVPrinter DGVPrinter = new DGVPrinter();
        ChartManager chartManager;
        bool isPanelOpen = true; // Переменная состояния панели (открыта или закрыта)

        public AllUsers()
        {
            db = new DB();
            InitializeComponent();
            _userservice = ServiceLocator.UserService;
            chartManager = new ChartManager();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void SetDataGridConfig()
        {
            DataGridViewColumn column0 = guna2DataGridView2.Columns["UserId"];
            column0.Visible = false;
        }

        private async void AllUsers_Load(object sender, EventArgs e)
        {
            await SetDataGrid();
            SetDataGridConfig();
            FillData();
           tabPage5.Visible = false;
          
        }

        private async Task SetDataGrid()
        {
            var users = await _userservice.GetUsersAsyc();
            guna2DataGridView2.DataSource = users;
        }

        private void gunaChart1_Load(object sender, EventArgs e)
        {
            FillData();
           
        }
        private void FillData()
        {
            var queryAllFeedback = "select * from [vw_Feedback]";
            db.ReturnData(queryAllFeedback, guna2DataGridView1);
            var queryAllMonitor = "exec [GetStatsWorkouts] @TypeOperation = 1";
            db.ReturnData(queryAllMonitor, guna2DataGridView3);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Объявляем скалярную переменную searchText и получаем текст из TextBox для поиска
                string searchText = guna2TextBox1.Text.Trim();

                // Формируем SQL-запрос для поиска по представлению vw_Feedback
                string query = $@"
        SELECT *
        FROM vw_Feedback
        WHERE 
            [Клиент] LIKE '%{searchText}%' OR
            [Оценка] LIKE '%{searchText}%' OR
            [Комментарий] LIKE '%{searchText}%' OR
            [Дата отправки отзыва] LIKE '%{searchText}%'";

                // Используем метод ReturnData для выполнения запроса и отображения результатов
                db.ReturnData(query, guna2DataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DGVPrinter.CreateReport($"Выгрузка {guna2ComboBox2.SelectedItem}", guna2DataGridView3);
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            var query = $"exec GetStatsWorkouts @TypeOperation = {guna2ComboBox2.SelectedIndex + 1}";
            if (db.ReturnData(query, guna2DataGridView3) != null)
            {
                chartManager.ChartBar(gunaChart1, ConvertDataGridToDataTable(guna2DataGridView3), guna2ComboBox2.SelectedItem.ToString());
                guna2TabControl2.SelectedIndex = 1;
                MessageBox.Show("Статистика успешно сформирована.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public DataTable ConvertDataGridToDataTable(DataGridView dataGridView)
        {
            DataTable dataTable = new DataTable();

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                dataTable.Columns.Add(column.Name, column.ValueType);
            }

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dataRow[i] = row.Cells[i].Value;
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            var query = $"exec GetStatsWorkouts @TypeOperation = {guna2ComboBox2.SelectedIndex + 1}";
            if (db.ReturnData(query, guna2DataGridView3) != null)
            {
                chartManager.ChartBar(gunaChart1, ConvertDataGridToDataTable(guna2DataGridView3), guna2ComboBox2.SelectedItem.ToString());
                guna2TabControl2.SelectedIndex = 1;
                MessageBox.Show("Статистика успешно сформирована.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        

        private async void pictureBox2_Click(object sender, EventArgs e)
        {
            EditUsers fromEditUsers = new EditUsers();
            fromEditUsers.Show();
        }
       
        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
