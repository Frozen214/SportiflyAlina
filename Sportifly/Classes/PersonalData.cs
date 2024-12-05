using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportifly.Classes
{
    internal class PersonalData
    {
        public static string Login { get; private set; } = "Логин";
        public static string Password { get; private set; } = "Пароль";
        public static string Role { get; private set; } = "3";
        public static string IdEmployeeOrClient { get; private set; } = "3";
        public static string Name { get; set; } = "Имя";
        public static string FatherName { get; private set; } = "Отчество";
        public static string Surname { get; private set; } = "Фамилия";
        public static string Email { get; private set; } 
        public static string NumTel { get; private set; } = "Номер телефона";
        public static string IdUsers { get; private set; } 
        public static string Image { get; private set; }

        public bool SetPersonalData(string login, string password)
        {
            var db = new DB();
            var getHash = new Hashing();

            string sqlExpression = "exec LoginUser @LoginUser = @Login, @HashPassword = @Password ";

            using (SqlConnection myCon = new SqlConnection(db.StringConn))
            {
                myCon.Open();

                using (SqlCommand command = new SqlCommand(sqlExpression, myCon))
                {
                    var hashPassword = getHash.Hash(password);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", hashPassword);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            Login = reader["Логин"].ToString();
                            Password = reader["Хэш пароль"].ToString();
                            Role = reader["Роль"].ToString();
                            IdEmployeeOrClient = reader["ID Клиента или Сотрудника"].ToString();
                            Name = reader["Имя"].ToString();
                            Surname = reader["Фамилия"].ToString();
                            FatherName = reader["Отчество"].ToString();
                            NumTel = reader["Номер телефона"].ToString();
                            IdUsers = reader["Пользователь"].ToString();
                            Email = reader["Почта"].ToString();
                            Image = reader["Фотография"].ToString();

                            return true;
                        }
                    }
                    return false;
                }
            }
        }
    }
}
