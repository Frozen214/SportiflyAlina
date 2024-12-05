using Sportifly.Classes;
using Sportifly.Model;
using System;
using System.Windows.Forms;

namespace Sportifly.User
{
    public partial class FormFeedback : Form
    {
        private FeedbackRepository FeedbackRepository { get; set; } = new FeedbackRepository();
        private FeedbackValidator FeedbackValidator { get; set; } = new FeedbackValidator();
        private byte BallUser = 0;

        public FormFeedback()
        {
            InitializeComponent();
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (BallUser == 0)
            {
                MessageBox.Show("Выберите оценку!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FeedbackModel feedbackModel = new FeedbackModel
            {
                Ball = BallUser,
                ClientId = Convert.ToInt32(PersonalData.UserId),
                FeedbackComments = guna2TextBox1.Text
            };

            if (!FeedbackValidator.Validate(feedbackModel))
            {
                return;
            }

            if (FeedbackRepository.CreateFeedback(feedbackModel))
            {
                guna2Button2.Enabled = false;
                guna2Button3.Enabled = false;
                guna2Button4.Enabled = false;
                guna2Button5.Enabled = false;
                guna2Button6.Enabled = false;
                guna2TextBox1.ReadOnly = true;
                MessageBox.Show("Отзыв успешно отправлен!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            BallUser = 1;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            BallUser = 2;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            BallUser = 3;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            BallUser = 4;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            BallUser = 5;
        }
    }
}
