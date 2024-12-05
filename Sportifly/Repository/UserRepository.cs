using Sportifly.Classes;
using Sportifly.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sportifly.Repository
{
    public class UserRepository
    {
        private Hashing Hashing { get; set; } = new Hashing();
        private DataBase dataBase { get; set; } = new DataBase();

        /// <summary>
        /// Получает все записи из таблицы ролей.
        /// </summary>
        /// <returns>DataTable с записями из таблицы ролей.</returns>
        public DataTable GetRoleAll()
        {
            try
            {
                var connection = new SqlConnection(dataBase.ConnectionString);
                connection.Open();

                var command = new SqlCommand("SELECT * FROM dbo.Роли", connection);
                command.CommandType = CommandType.Text;

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при получении записей из таблицы Роли.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        /// <summary>
        /// Обновляет информацию о пользователе в базе данных.
        /// </summary>
        /// <param name="user">Модель пользователя с обновленными данными.</param>
        /// <returns>Возвращает true, если данные успешно обновлены; иначе false.</returns>
        public bool UpdateUser(UserModel user)
        {
            var userValidator = new UserValidator();

            if (!userValidator.EmailValidation(user.UserLogin))
            {
                return false;
            }
            if (!userValidator.PasswordValidation(user.UserPassword))
            {
                return false;
            }

            try
            {
                var connection = new SqlConnection(dataBase.ConnectionString);
                connection.Open();

                var command = new SqlCommand("dbo.UpdateUser", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserId", user.UserId);
                command.Parameters.AddWithValue("@Login", user.UserLogin);
                command.Parameters.AddWithValue("@Password", Hashing.Hash(user.UserPassword));
                command.Parameters.AddWithValue("@RoleId", user.RoleId);

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
        /// Создает нового пользователя в базе данных.
        /// </summary>
        /// <param name="user">Модель пользователя, содержащая информацию для создания.</param>
        /// <returns>Возвращает true, если пользователь успешно создан; иначе false.</returns>
        public bool CreateUser(UserModel user)
        {
            var userValidator = new UserValidator();

            if (!userValidator.EmailValidation(user.UserLogin))
            {
                return false;
            }
            if (!userValidator.PasswordValidation(user.UserPassword))
            {
                return false;
            }

            try
            {
                var connection = new SqlConnection(dataBase.ConnectionString);
                connection.Open();

                var command = new SqlCommand("dbo.CreateUser", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Login", user.UserLogin);
                command.Parameters.AddWithValue("@Password", Hashing.Hash(user.UserPassword));
                command.Parameters.AddWithValue("@RoleId", user.RoleId);

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
        /// Удаляет пользователя и очищает его связи с таблицами.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, которого нужно удалить.</param>
        /// <returns>Возвращает true, если пользователь успешно удален; иначе false.</returns>
        public bool DeleteUser(int userId)
        {
            try
            {
                var connection = new SqlConnection(dataBase.ConnectionString);
                connection.Open();

                var command = new SqlCommand("dbo.DeleteUser", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserId", userId);

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
        /// Свободные аккаунты для передачи клиентам/сотрудникам
        /// </summary>
        /// <returns></returns>
        public DataTable GetFreeAccount()
        {
            try
            {
                var connection = new SqlConnection(dataBase.ConnectionString);
                connection.Open();

                var command = new SqlCommand("SELECT * FROM dbo.vw_FreeAccount", connection);
                command.CommandType = CommandType.Text;

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при получении записей из таблицы Роли.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }
    }
}
