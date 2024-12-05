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
    public partial class Login : Form
    {


        private bool isVisiblePassword = false;
        private static string BasePath = BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../");
        public Login()
        {
            InitializeComponent();
        }
        Image ViewImage = Image.FromFile($@"{BasePath}макет\view.png");
        Image HideImage = Image.FromFile($@"{BasePath}макет\hide.png");
        private void materialButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Registr Registr = new Registr();
            Registr.Show();
            this.Hide();

        }

        private void guna2TextBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (isVisiblePassword == true)
            {
                isVisiblePassword = false;
                pictureBox2.Image = HideImage;
                guna2TextBox2.UseSystemPasswordChar = true;
            }
            else
            {
                isVisiblePassword = true;
                pictureBox2.Image = ViewImage;
                guna2TextBox2.UseSystemPasswordChar = false;
            }
        }
    }
}
