using Sportifly.Model;
using System.Text.RegularExpressions;

namespace Sportifly.Validator
{
    public class EmployeeValidator
    {
        /// <summary>
        /// Валидация модели сотрудника.
        /// </summary>
        /// <param name="employeeModel">Модель сотрудника для валидации.</param>
        /// <param name="validationMessage">Сообщение об ошибке, если валидация не прошла.</param>
        /// <returns>True, если данные модели валидны, иначе False.</returns>
        public bool Validate(EmployeeModel employeeModel, out string validationMessage)
        {
            validationMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(employeeModel.LastName))
            {
                validationMessage = "Фамилия обязательна для указания.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(employeeModel.FirstName))
            {
                validationMessage = "Имя обязательно для указания.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(employeeModel.MiddleName))
            {
                validationMessage = "Отчество обязательно для указания.";
                return false;
            }

            if (employeeModel.LastName.Length > 50)
            {
                validationMessage = "Фамилия не должна превышать 50 символов.";
                return false;
            }

            if (employeeModel.FirstName.Length > 50)
            {
                validationMessage = "Имя не должно превышать 50 символов.";
                return false;
            }

            if (employeeModel.MiddleName.Length > 50)
            {
                validationMessage = "Отчество не должно превышать 50 символов.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(employeeModel.Email))
            {
                validationMessage = "Email обязателен для указания.";
                return false;
            }

            if (!IsValidEmail(employeeModel.Email))
            {
                validationMessage = "Email имеет некорректный формат.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(employeeModel.Direction))
            {
                validationMessage = "Направление обязательно для указания.";
                return false;
            }

            if (employeeModel.Direction.Length > 100)
            {
                validationMessage = "Направление не должно превышать 100 символов.";
                return false;
            }

            if (employeeModel.UserId == -1)
            {
                validationMessage = "Идентификатор пользователя не может быть равен -1.";
                return false;
            }

            if (!int.TryParse(employeeModel.Experience, out int experience) || experience < 0 || experience > 99)
            {
                validationMessage = "Опыт работы должен быть целым числом от 0 до 99.";
                return false;
            }

            if (!IsValidPhoneNumber(employeeModel.PhoneNumber))
            {
                validationMessage = "Номер телефона имеет некорректный формат.";
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            return Regex.IsMatch(phoneNumber, @"^\+7\d{10}$");
        }
    }
}
