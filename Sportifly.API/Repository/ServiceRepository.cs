using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Sportifly.API.Model;

namespace Sportifly.API.Repository
{
    public class ServiceRepository
    {
        private readonly string _connectionString;

        public ServiceRepository(IOptions<AppSettings> options)
        {
            _connectionString = options.Value.ConnectionStrings.MsSqlConnection;
        }

        public async Task<IEnumerable<ServiceModel>> GetServiceAll()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return await connection.QueryAsync<ServiceModel>("SELECT у.[ID услуги] AS ServiceId," +
                " у.Название AS ServiceName," +
                " у.Описание AS Description," +
                " у.Цена AS Price ," +
                "у.Тренер AS Coach" +
                " FROM dbo.Услуги у");
        }
    }
}
