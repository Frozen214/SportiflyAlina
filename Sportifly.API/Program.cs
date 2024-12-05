using Microsoft.EntityFrameworkCore;
using Sportifly.API.Infrastructure;
using Sportifly.API.Interface;
using Sportifly.API.Repository;
using Sportifly.API;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddDbContext<SqlDbContext>(options =>
    options.UseSqlServer(configuration.ConnectionStrings.MsSqlConnection));

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = true).AddJsonOptions(x =>
{
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
