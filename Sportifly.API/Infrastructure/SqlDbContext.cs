using Microsoft.EntityFrameworkCore;

namespace Sportifly.API.Infrastructure;

public class SqlDbContext(DbContextOptions<SqlDbContext> configure) : DbContext(configure)
{
}
