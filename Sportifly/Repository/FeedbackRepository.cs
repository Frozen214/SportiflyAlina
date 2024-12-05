using Sportifly.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sportifly.Classes
{
    public class FeedbackRepository
    {
        private DataBase DB { get; set; } = new DataBase();
        private FeedbackValidator FeedbackValidator { get; set; } = new FeedbackValidator();

        /// <summary>
        /// Создает новый отзыв в базе данных.
        /// </summary>
        /// <param name="feedback">Модель отзыва, содержащая информацию для создания.</param>
        /// <returns>Возвращает true, если отзыв успешно добавлен; иначе false.</returns>
        public bool CreateFeedback(FeedbackModel feedback)
        {
            if (!FeedbackValidator.Validate(feedback))
            {
                return false;
            }

            try
            {
                var connection = new SqlConnection(DB.ConnectionString);
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Отзывы(Клиент, Комментарий, Рейтинг, Дата) values (@ClientId, @FeedbackComments, @Ball, @DateTime)", connection))
                {
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@ClientId", feedback.ClientId);
                    command.Parameters.AddWithValue("@FeedbackComments", feedback.FeedbackComments);
                    command.Parameters.AddWithValue("@Ball", feedback.Ball);
                    command.Parameters.AddWithValue("@DateTime", DateTime.Now);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
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