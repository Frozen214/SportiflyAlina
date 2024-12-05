using Sportifly.Admin;
using Sportifly.Classes;
using Sportifly.Common;
using Sportifly.Infrastructure;
using Sportifly.Interface;
using Sportifly.Model;
using Sportifly.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sportifly
{
    public partial class AllUsers : Form
    {
        private readonly IUserService _userservice;
        private DataBase db { get; set; } = new DataBase();
        private DGVPrinter DGVPrinter { get; set; } = new DGVPrinter();
        private ChartManager chartManager { get; set; } = new ChartManager();
        private UserRepository userRepository { get; set; } = new UserRepository();
        private ConfirmAction ConfirmAction { get; set; } = new ConfirmAction();
        private BindingSource UsersBindingSource { get; set; } = new BindingSource();
        private BindingSource EmployeeBindingSource { get; set; } = new BindingSource();
        private List<UserModel> UserList { get; set; } = new List<UserModel>();
        private EmployeeRepository EmployeeRepository { get; set; } = new EmployeeRepository();

        public AllUsers()
        {
            InitializeComponent();
            _userservice = ServiceLocator.UserService;
        }

        private void UserDataGridVisible()
        {
            DataGridViewColumn column0 = guna2DataGridView2.Columns["UserId"];
            column0.Visible = false;
            DataGridViewColumn column1 = guna2DataGridView2.Columns["RoleId"];
            column1.Visible = false;
        }

        private async void AllUsers_Load(object sender, EventArgs e)
        {
            await LoadDataGrid();
            UserDataGridVisible();
            EmployeeDataGridVisible();
            tabPage5.Visible = false;
        }

        public async Task LoadDataGrid()
        {
            UserList = await _userservice.GetUsersAsyc();
            UsersBindingSource.DataSource = UserList;
            guna2DataGridView2.DataSource = UsersBindingSource;

            RenamingColumn();

            var queryAllFeedback = "select * from [vw_Feedback]";
            db.ReturnData(queryAllFeedback, guna2DataGridView1);

            var queryAllMonitor = "exec [GetStatsWorkouts] @TypeOperation = 1";
            db.ReturnData(queryAllMonitor, guna2DataGridView3);

            EmployeeBindingSource.DataSource = EmployeeRepository.GetEmployee();
            guna2DataGridView5.DataSource = EmployeeBindingSource;
        }

        private void RenamingColumn()
        {
            guna2DataGridView2.Columns["UserLogin"].HeaderText = "Логин";
            guna2DataGridView2.Columns["UserOwner"].HeaderText = "Владелец";
            guna2DataGridView2.Columns["UserRole"].HeaderText = "Роль";
            guna2DataGridView2.Columns["UserPassword"].HeaderText = "Пароль";
            guna2DataGridView2.Columns["UserId"].HeaderText = "ID Пользователя";
            guna2DataGridView2.Columns["RoleId"].HeaderText = "ID Роли";
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var searchText = guna2TextBox1.Text.Trim();
            var query = $@"SELECT *
                                FROM vw_Feedback
                                WHERE 
                                    [Клиент] LIKE '%{searchText}%' OR
                                    [Оценка] LIKE '%{searchText}%' OR
                                    [Комментарий] LIKE '%{searchText}%' OR
                                    [Дата отправки отзыва] LIKE '%{searchText}%'";

            db.ReturnData(query, guna2DataGridView1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DGVPrinter.CreateReport($"Выгрузка {guna2ComboBox2.SelectedItem}", guna2DataGridView3);
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!IsSelecedRowUser())
            {
                return;
            }

            DataGridViewRow selectedRow = guna2DataGridView2.SelectedRows[0];
            var user = new UserModel()
            {
                UserId = (int)selectedRow.Cells["UserId"].Value,
                UserRole = selectedRow.Cells["UserRole"].Value.ToString(),
                UserLogin = selectedRow.Cells["UserLogin"].Value.ToString(),
                RoleId = (int)selectedRow.Cells["RoleId"].Value,
                UserPassword = string.Empty
            };
            EditUsers fromEditUsers = new EditUsers(2, user, this);
            fromEditUsers.Show();
        }

        private bool IsSelecedRowUser()
        {
            if (guna2DataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            EditUsers fromEditUsers = new EditUsers(1, new UserModel(), this);
            fromEditUsers.Show();
        }

        private async void pictureBox6_Click(object sender, EventArgs e)
        {
            if (!IsSelecedRowUser())
            {
                return;
            }

            if (!ConfirmAction.ShowConfirmationDialog("Вы действительно хотите удалить пользователя?", "Подтверждение"))
            {
                MessageBox.Show("Действие отменено.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow selectedRow = guna2DataGridView2.SelectedRows[0];
            if (userRepository.DeleteUser((int)selectedRow.Cells["UserId"].Value))
            {
                await LoadDataGrid();
                MessageBox.Show("Пользователь успешно удален!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            FilterData(guna2TextBox7.Text.Trim().ToLower());
        }

        private void FilterData(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                UsersBindingSource.RemoveFilter();
                return;
            }
            var filteredUsers = UserList
              .Where(user => user.UserLogin.ToLower().Contains(searchText) ||
                             user.UserOwner.ToLower().Contains(searchText) ||
                             user.UserRole.ToLower().Contains(searchText))
              .ToList();

            UsersBindingSource.DataSource = filteredUsers;
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            FilterDataEmployee(guna2TextBox3.Text.Trim().ToLower());
        }

        private void FilterDataEmployee(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                EmployeeBindingSource.RemoveFilter();
            }
            else
            {
                EmployeeBindingSource.Filter = $"CONVERT([Сотрудник], 'System.String') LIKE '%{searchText}%' OR " +
                                       $"CONVERT([Направление], 'System.String') LIKE '%{searchText}%' OR " +
                                       $"CONVERT([Опыт работы], 'System.String') LIKE '%{searchText}%' OR " +
                                       $"CONVERT([Почта], 'System.String') LIKE '%{searchText}%' ";
            }
        }

        private void EmployeeDataGridVisible()
        {
            DataGridViewColumn column0 = guna2DataGridView5.Columns["ID Тренера"];
            column0.Visible = false;
            DataGridViewColumn column1 = guna2DataGridView5.Columns["Фамилия"];
            column1.Visible = false;
            DataGridViewColumn column2 = guna2DataGridView5.Columns["Имя"];
            column2.Visible = false;
            DataGridViewColumn column3 = guna2DataGridView5.Columns["Отчество"];
            column3.Visible = false;
            DataGridViewColumn column4 = guna2DataGridView5.Columns["Фотография"];
            column4.Visible = false;
            DataGridViewColumn column5 = guna2DataGridView5.Columns["Пользователь"];
            column5.Visible = false;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            var formEdit = new FormEditEmployee(1, new EmployeeModel(), this);
            formEdit.ShowDialog();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (!IsSelecedRowEmployee())
            {
                return;
            }

            DataGridViewRow selectedRow = guna2DataGridView5.SelectedRows[0];
            var employee = new EmployeeModel()
            {
                LastName = selectedRow.Cells["Фамилия"].Value.ToString(),
                FirstName = selectedRow.Cells["Имя"].Value.ToString(),
                MiddleName = selectedRow.Cells["Отчество"].Value.ToString(),
                PhoneNumber = selectedRow.Cells["Номер телефона"].Value.ToString(),
                Email = selectedRow.Cells["Почта"].Value.ToString(),
                Direction = selectedRow.Cells["Направление"].Value.ToString(),
                Experience = selectedRow.Cells["Опыт работы"].Value.ToString(),
                EmployeeId = (int)selectedRow.Cells["ID Тренера"].Value
            };

            var formEdit = new FormEditEmployee(2, employee, this);
            formEdit.ShowDialog();
        }

        private bool IsSelecedRowEmployee()
        {
            if (guna2DataGridView5.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите сотрудника.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private async void pictureBox7_Click(object sender, EventArgs e)
        {
            if (!IsSelecedRowEmployee())
            {
                return;
            }

            if (!ConfirmAction.ShowConfirmationDialog("Вы действительно хотите уволить сотрудника?", "Подтверждение"))
            {
                MessageBox.Show("Действие отменено.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }

            DataGridViewRow selectedRow = guna2DataGridView5.SelectedRows[0];
            if (EmployeeRepository.DeleteEmployee((int)selectedRow.Cells["ID Тренера"].Value))
            {
                await LoadDataGrid();
                MessageBox.Show("Сотрудник успешно уволен!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
