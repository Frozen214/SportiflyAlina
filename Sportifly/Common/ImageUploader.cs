using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Sportifly.Classes
{
    public class ImageUploader
    {
        private DataBase db {  get; set; } = new DataBase();

        public void Upload(PictureBox pictureBox, int userId)
        {
            using (var connection = new SqlConnection(db.ConnectionString))
            using (var command = connection.CreateCommand())
            {
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

                    command.Parameters.AddWithValue("@IdUsers", Convert.ToInt32(PersonalData.UserId));
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    internal class ImageRetriever
    {
        private DataBase db { get; set; } = new DataBase();

        public void Retrieve(PictureBox pictureBox, int userId)
        {
            using (var connection = new SqlConnection(db.ConnectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Фотография FROM Клиенты WHERE Пользователь = @IdUsers";
                command.Parameters.AddWithValue("@IdUsers", Convert.ToInt32(PersonalData.UserId));

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
