using Dapper;
using Microsoft.Data.SqlClient;
using Sportifly.API.Interface;
using Sportifly.API.Model;

namespace Sportifly.API.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private string ConnectionString;
        public ServiceRepository(IConfiguration configuration)
        {
            //var configuration = new ConfigurationBuilder()  
            //.SetBasePath(Directory.GetCurrentDirectory())
            // .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //.Build();
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<ServiceModel> GetServices()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection.Query<ServiceModel>("select * from Услуги");
        }
    }
}
