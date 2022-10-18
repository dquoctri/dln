using Microsoft.EntityFrameworkCore;
using Repository.Common;
using Uzer.Api.Extensions;
using Uzer.Api.Services;
using Uzer.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);

builder.Services.AddDefaultDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));
// Add services to the container.
#region Repositories
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

#endregion

builder.Services.AddAsymmetricAuthentication();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfiguringSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCustomSwaggerUI("docs/uzer");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/api/error");
}
app.UseAuthorization();

app.MapControllers();

app.Run();
