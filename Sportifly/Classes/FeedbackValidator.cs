using Sportifly.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sportifly.Classes
{
    public class FeedbackValidator
    {
        public bool Validate(FeedbackModel feedback)
        {
            if (string.IsNullOrWhiteSpace(feedback.FeedbackComments))
            {
                MessageBox.Show("Текст отзыва не может быть пустым.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            string[] words = feedback.FeedbackComments.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length < 7)
            {
                MessageBox.Show("Текст отзыва должен содержать не менее 7 слов.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (feedback.FeedbackComments.Length > 150)
            {
                MessageBox.Show("Текст отзыва должен содержать не более 50 слов.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }

}

