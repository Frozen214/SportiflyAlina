using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Sportifly.Classes
{
    internal class ImageUploader
    {
        private readonly string _connectionString;

        public ImageUploader(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Upload(PictureBox pictureBox, int userId)
        {
            var db = new DB();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                // Обновляем фотографию для клиента с определенным пользователем
                command.CommandText = "UPDATE Клиенты SET Фотография = @Image WHERE Пользователь = @IdUsers";

                var image = new Bitmap(pictureBox.Image);
                using (var memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, ImageFormat.Jpeg);
                    memoryStream.Position = 0;

                    var sqlParameter = new SqlParameter("@Image", SqlDbType.VarBinary, (int)memoryStream.Length)
                    {
                        Value = memoryStream.ToArray()
                    };
                    command.Parameters.Add(sqlParameter);

                    // Добавляем параметр для ID пользователя
                    command.Parameters.AddWithValue("@IdUsers", Convert.ToInt32(PersonalData.IdUsers));
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    internal class ImageRetriever
    {
        private readonly string _connectionString;

        public ImageRetriever(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Retrieve(PictureBox pictureBox, int userId)
        {
            var db = new DB();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                // Извлекаем фотографию клиента по идентификатору пользователя
                command.CommandText = "SELECT Фотография FROM Клиенты WHERE Пользователь = @IdUsers";
                command.Parameters.AddWithValue("@IdUsers", Convert.ToInt32(PersonalData.IdUsers));

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read() && reader ["Фотография"] != DBNull.Value )
                    {
                        var imageData = (byte[])reader["Фотография"];
                        using (var memoryStream = new MemoryStream(imageData))
                        {
                            pictureBox.Image = Image.FromStream(memoryStream);
                        }
                    }
                    else
                    {
                        pictureBox.Image = null;
                    }
                    
                }
            }
        }
    }
}
