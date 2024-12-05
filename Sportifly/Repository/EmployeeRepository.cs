using Sportifly.Classes;
using Sportifly.Model;
using Sportifly.Validator;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sportifly.Repository
{
    public class EmployeeRepository
    {
        private DataBase dataBase { get; set; } = new DataBase();
        /// <summary>
        /// Добавляет нового сотрудника в базу данных.
        /// </summary>
        /// <param name="employee">Модель сотрудника, содержащая информацию для добавления.</param>
        /// <returns>Возвращает true, если сотрудник успешно добавлен; иначе false.</returns>
        public bool AddEmployee(EmployeeModel employee)
        {
            var employeeValidator = new EmployeeValidator();
            if (!employeeValidator.Validate(employee, out string validationMessage))
            {
                MessageBox.Show(validationMessage, "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                SqlConnection connection = new SqlConnection(dataBase.ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("dbo.CreateEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                command.Parameters.AddWithValue("@Direction", employee.Direction);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Experience", employee.Experience);
                command.Parameters.AddWithValue("@UserId", employee.UserId);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Ошибка базы данных: {sqlEx.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Удаляет сотрудника из базы данных.
        /// </summary>
        /// <param name="employeeId">Уникальный идентификатор сотрудника для удаления.</param>
        /// <returns>Возвращает true, если сотрудник успешно удален; иначе false.</returns>
        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(dataBase.ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("dbo.DeleteEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Ошибка базы данных: {sqlEx.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Обновляет данные сотрудника в базе данных.
        /// </summary>
        /// <param name="employee">Модель сотрудника, содержащая обновленные данные.</param>
        /// <returns>Возвращает true, если данные сотрудника успешно обновлены; иначе false.</returns>
        public bool UpdateEmployee(EmployeeModel employee)
        {
            var employeeValidator = new EmployeeValidator();
            if (!employeeValidator.Validate(employee, out string validationMessage))
            {
                MessageBox.Show(validationMessage, "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                SqlConnection connection = new SqlConnection(dataBase.ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("dbo.UpdateEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                command.Parameters.AddWithValue("@Direction", employee.Direction);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Experience", employee.Experience);
                command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Ошибка базы данных: {sqlEx.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataTable GetEmployee()
        {
            try
            {
                SqlConnection connection = new SqlConnection(dataBase.ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM dbo.vw_Employee", connection);
                command.CommandType = CommandType.Text;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при получении записей из vw_Employee.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }
    }
}
