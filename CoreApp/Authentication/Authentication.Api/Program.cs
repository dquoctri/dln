
using Microsoft.EntityFrameworkCore;
using Authentication.Api.Extensions;
using Authentication.Api.Models;
using Authentication.Api.Services;
using Authentication.Context;
using Microsoft.EntityFrameworkCore.Migrations;
using Repository.Common;
using Authentication.Repository;

var builder = WebApplication.CreateBuilder(args);
var secretOptions = builder.Configuration.GetSection(SecretOptions.CONFIG_KEY);
var serect = secretOptions.Get<SecretOptions>();
builder.Services.Configure<SecretOptions>(secretOptions);

//builder.Configuration
//    .AddJsonFile("appsettings.json")
//    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);

//builder.Services.AddDefaultDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));
//// Add services to the container.
#region Repositories
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPartnerRepository, PartnerRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<SecretOptions>();

#endregion

//builder.Services.AddAsymmetricAuthentication();


//builder.Services.AddDefaultDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddDbContext<AuthenticationContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, AuthenticationContext.SCHEMA)));

builder.Services.AddRefreshAuthentication(serect);

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfiguringSwagger();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
