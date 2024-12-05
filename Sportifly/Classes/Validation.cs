using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Sportifly.Classes
{
    internal class Validation
    {
        public bool ValidationEmail (string email)
        {
            
            // Удаляем пробелы в начале и конце строки
            email = email.Trim();

            // Простое регулярное выражение для проверки правильности email
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            // Проверка email на корректность
            if (!emailRegex.IsMatch(email))
            {
                MessageBox.Show("Неверный формат почты!", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public bool ValidationPass(string password)
        {

            var input = password;

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Поле с паролем пустое", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            } 
            // Если проверка прошла успешно
            return true;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{6,12}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            
            if (!hasLowerChar.IsMatch(input))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну строчную букву", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну заглавную букву", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                MessageBox.Show("Пароль должен быть длиннее 6 символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одно числовое значение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
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
