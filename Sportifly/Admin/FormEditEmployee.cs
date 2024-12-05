using Sportifly.Model;
using Sportifly.Repository;
using System;
using System.Windows.Forms;

namespace Sportifly.Admin
{
    public partial class FormEditEmployee : Form
    {
        private EmployeeModel EmployeeModel { get; set; }
        private AllUsers FormEmployee { get; set; }
        private int Option { get; set; }
        private EmployeeRepository EmployeeRepository { get; set; } = new EmployeeRepository();
        private UserRepository UserRepository { get; set; } = new UserRepository();

        public FormEditEmployee(int option, EmployeeModel employeeModel, AllUsers formEmployee)
        {
            InitializeComponent();
            EmployeeModel = employeeModel;
            Option = option;
            FormEmployee = formEmployee;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            EmployeeModel employee = new EmployeeModel()
            {
                LastName = guna2TextBox1.Text,
                FirstName = guna2TextBox11.Text,
                MiddleName = guna2TextBox2.Text,
                Experience = guna2TextBox6.Text,
                PhoneNumber = guna2TextBox3.Text,
                Email = guna2TextBox4.Text,
                Direction = guna2TextBox5.Text,
                EmployeeId = EmployeeModel.EmployeeId
            };


            if (Option == 1)
            {
                employee.UserId = (int)guna2ComboBox2.SelectedValue;

                if (EmployeeRepository.AddEmployee(employee))
                {
                    MessageBox.Show("Сотрудник успешно создан!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormEmployee.LoadDataGrid();
                    this.Close();
                    return;
                }
            }
            else
            {
                if (EmployeeRepository.UpdateEmployee(employee))
                {
                    MessageBox.Show("Сотрудник успешно изменен!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormEmployee.LoadDataGrid();
                    this.Close();
                    return;
                }
            }
        }

        private void FormEditEmployee_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            if (Option == 1)
            {
                guna2Button1.Text = "Добавить";
                label3.Text = "Добавление";
            }
            else
            {
                guna2ComboBox2.Visible = false;
                label1.Visible = false;

                guna2TextBox1.Text = EmployeeModel.LastName;
                guna2TextBox11.Text = EmployeeModel.FirstName;
                guna2TextBox2.Text = EmployeeModel.MiddleName;
                guna2TextBox6.Text = EmployeeModel.Experience;
                guna2TextBox3.Text = EmployeeModel.PhoneNumber;
                guna2TextBox4.Text = EmployeeModel.Email;
                guna2TextBox5.Text = EmployeeModel.Direction;
                guna2Button1.Text = "Сохранить";
                label3.Text = "Изменение";
            }
        }

        private void LoadCombobox()
        {
            guna2ComboBox2.DataSource = UserRepository.GetFreeAccount();
            guna2ComboBox2.DisplayMember = "Логин";
            guna2ComboBox2.ValueMember = "ID Пользователя";
            guna2ComboBox2.SelectedIndex = 0;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
