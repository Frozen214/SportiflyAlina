using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Sportifly.API.Interface;
using Sportifly.API.Model;

namespace Sportifly.API.Repository;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IOptions<AppSettings> options)
    {
        _connectionString = options.Value.ConnectionStrings.MsSqlConnection;
    }

    public async Task<IEnumerable<UserModel>> GetUserAll()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryAsync<UserModel>(@"
                    SELECT [ID Пользователя] AS [UserId]
                     ,Логин as [UserLogin]
                     ,[Хэш пароль] AS [UserPassword]
                     ,Роль AS [UserRole]
                     ,[Владелец аккаунта] AS [UserOwner]
                    FROM vw_Users");
    }
}
