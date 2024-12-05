using System.Windows.Forms;

namespace Sportifly.Common
{
    public class ConfirmAction
    {
        public bool ShowConfirmationDialog(string message, string caption)
        {
            DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }
    }
}
