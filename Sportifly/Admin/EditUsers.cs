using Sportifly.Classes;
using Sportifly.Model;
using Sportifly.Repository;
using System;
using System.Windows.Forms;

namespace Sportifly.Admin
{
    public partial class EditUsers : Form
    {
        private DataBase db { get; set; } = new DataBase();
        private UserRepository UserRepository { get; set; } = new UserRepository();
        private UserModel UserModel { get; set; }
        private AllUsers AllUsers { get; set; }
        private int Option { get; set; }

        public EditUsers(int option, UserModel userModel, AllUsers allUsers)
        {
            InitializeComponent();
            AllUsers = allUsers;
            UserModel = userModel;
            Option = option;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void EditUsers_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            if (Option == 1)
            {
                guna2Button5.Text = "Добавить";
                label1.Text = "Добавление";
            }
            else
            {
                guna2TextBox11.Text = UserModel.UserLogin;
                guna2TextBox10.Text = UserModel.UserPassword;
                guna2ComboBox1.SelectedValue = UserModel.RoleId;
                guna2Button5.Text = "Изменить";
                label1.Text = "Изменение";
            }
        }

        private void LoadCombobox()
        {
            guna2ComboBox1.DataSource = UserRepository.GetRoleAll();
            guna2ComboBox1.DisplayMember = "Название роли";
            guna2ComboBox1.ValueMember = "ID Роли";
            guna2ComboBox1.SelectedIndex = 0;
        }

        private async void guna2Button5_Click(object sender, EventArgs e)
        {
            UserModel user = new UserModel()
            {
                UserId = UserModel.UserId,
                UserPassword = guna2TextBox10.Text,
                UserLogin = guna2TextBox11.Text,
                RoleId = (int)guna2ComboBox1.SelectedValue,
            };

            if (Option == 1)
            {
                if (UserRepository.CreateUser(user))
                {
                    await AllUsers.LoadDataGrid();
                    MessageBox.Show("Пользователь успешно создан!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
            }
            else
            {
                if (UserRepository.UpdateUser(user))
                {
                    await AllUsers.LoadDataGrid();
                    MessageBox.Show("Пользователь успешно изменен!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
            }
        }
    }
}
