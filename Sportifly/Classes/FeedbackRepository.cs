using Sportifly.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sportifly.Classes
{
    public class FeedbackRepository
    {
        private DB DB { get; set; }
        private FeedbackValidator FeedbackValidator { get; set; }

        public FeedbackRepository()
        {
            // Инициализируем свойства в конструкторе, поскольку C# 8.0+ инициализация не поддерживается
            DB = new DB();
            FeedbackValidator = new FeedbackValidator();
        }

        /// <summary>
        /// Создает новый отзыв в базе данных.
        /// </summary>
        /// <param name="feedback">Модель отзыва, содержащая информацию для создания.</param>
        /// <returns>Возвращает true, если отзыв успешно добавлен; иначе false.</returns>
        public bool CreateFeedback(FeedbackModel feedback)
        {
            try
            {
                if (!FeedbackValidator.Validate(feedback))
                {
                    return false;
                }

                // Инициализируем SqlConnection с использованием конструкции try-finally для совместимости
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection(DB.StringConn);
                    connection.Open();

                    // Используем команду SQL
                    using (SqlCommand command = new SqlCommand("INSERT INTO Отзывы(Клиент, Комментарий, Рейтинг, Дата) values (@ClientId, @FeedbackComments, @Ball, @DateTime)", connection))
                    {
                        command.CommandType = CommandType.Text;

                        // Добавляем параметры
                        command.Parameters.AddWithValue("@ClientId", feedback.ClientId);
                        command.Parameters.AddWithValue("@FeedbackComments", feedback.FeedbackComments);
                        command.Parameters.AddWithValue("@Ball", feedback.Ball);
                        command.Parameters.AddWithValue("@DateTime", DateTime.Now);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
                finally
                {
                    // Закрываем соединение, если оно было открыто
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении отзыва. {ex}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
