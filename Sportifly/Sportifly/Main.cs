using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sportifly
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

        }
        private Form acriveForm = null;
        private void openForm(Form childForm)

        {
            if (acriveForm != null)
                acriveForm.Close();
            acriveForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void CheckRole(string role)
        {
            switch (role.ToLower())
            {
                case "клиент":
                    label5.Dispose();
                    label2.Dispose();
                    break;
                case "тренер":
                    label5.Dispose();
                    break;
                case "администратор":
                    label5.Dispose();
                    break;
                default:
                    MessageBox.Show("Роль некорректна!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
        }
    }
}
