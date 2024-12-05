using System.Data.SqlClient;

namespace Sportifly.Classes
{
    public class PersonalData
    {
        public static string Login { get; private set; } = "Логин";
        public static string Password { get; private set; } = "Пароль";
        public static string Role { get; private set; } = "3";
        public static string PersonId { get; private set; } = "4";
        public static string Name { get; set; } = "Имя";
        public static string FatherName { get; private set; } = "Отчество";
        public static string Surname { get; private set; } = "Фамилия";
        public static string Email { get; private set; } 
        public static string TelephoneNumber { get; private set; } = "Номер телефона";
        public static string UserId { get; private set; } 
        public static string Image { get; private set; }

        public bool SetPersonalData(string login, string password)
        {
            var db = new DataBase();
            var getHash = new Hashing();

            string sqlExpression = "exec LoginUser @LoginUser = @Login, @HashPassword = @Password ";

            using (SqlConnection myCon = new SqlConnection(db.ConnectionString))
            {
                myCon.Open();

                using (SqlCommand command = new SqlCommand(sqlExpression, myCon))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", getHash.Hash(password));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            ReadPersonalData(reader);
                            return true;
                        }
                    }
                    return false;
                }
            }
        }

        private void ReadPersonalData(SqlDataReader reader)
        {
            Login = reader["Логин"].ToString();
            Password = reader["Хэш пароль"].ToString();
            Role = reader["Роль"].ToString();
            PersonId = reader["ID Клиента или Сотрудника"].ToString();
            Name = reader["Имя"].ToString();
            Surname = reader["Фамилия"].ToString();
            FatherName = reader["Отчество"].ToString();
            TelephoneNumber = reader["Номер телефона"].ToString();
            UserId = reader["Пользователь"].ToString();
            Email = reader["Почта"].ToString();
            Image = reader["Фотография"].ToString();
        }
    }
}
