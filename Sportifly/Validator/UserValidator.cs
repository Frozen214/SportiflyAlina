using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sportifly.Classes
{
    public class UserValidator
    {
        public bool EmailValidation (string email)
        {
            email = email.Trim();

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (!emailRegex.IsMatch(email))
            {
                MessageBox.Show("Неверный формат почты!", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool PasswordValidation(string password)
        {
            if (string.IsNullOrWhiteSpace((string)password))
            {
                MessageBox.Show("Поле с паролем пустое", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            } 

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{6,12}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            
            if (!hasLowerChar.IsMatch((string)password))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну строчную букву", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (!hasUpperChar.IsMatch((string)password))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну заглавную букву", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch((string)password))
            {
                MessageBox.Show("Пароль должен быть длиннее 6 символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (!hasNumber.IsMatch((string)password))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одно числовое значение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (!hasSymbols.IsMatch((string)password))
            {
                MessageBox.Show("Пароль должен содержать хотя бы один спец. символ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
