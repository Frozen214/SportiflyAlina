using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sportifly
{
    public partial class Registr : Form
    {

        private bool isVisiblePassword = false;
        private static string BasePath = BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../");


        public Registr()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
        }

        Image ViewImage = Image.FromFile($@"{BasePath}макет\view.png");
        Image HideImage = Image.FromFile($@"{BasePath}макет\hide.png");

        private void label2_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (isVisiblePassword == true)
            {
                isVisiblePassword = false;
                pictureBox2.Image = HideImage;
                guna2TextBox5.UseSystemPasswordChar = true;
            }
            else
            {
                isVisiblePassword = true;
                pictureBox2.Image = ViewImage;
                guna2TextBox5.UseSystemPasswordChar = false;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (isVisiblePassword == true)
            {
                isVisiblePassword = false;
                pictureBox2.Image = HideImage;
                guna2TextBox6.UseSystemPasswordChar = true;
            }
            else
            {
                isVisiblePassword = true;
                pictureBox2.Image = ViewImage;
                guna2TextBox6.UseSystemPasswordChar = false;
            }
        }
    }
}
