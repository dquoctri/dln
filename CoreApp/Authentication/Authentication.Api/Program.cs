
using Microsoft.EntityFrameworkCore;
using Authentication.Api.Extensions;
using Authentication.Api.Models;
using Authentication.Api.Services;
using Authentication.Context;
using Microsoft.EntityFrameworkCore.Migrations;
using Repository.Common;
using Authentication.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);

var secretOptions = builder.Configuration.GetSection(SecretOptions.CONFIG_KEY);
var serect = secretOptions.Get<SecretOptions>();
builder.Services.Configure<SecretOptions>(secretOptions);

builder.Services.AddDbContext<AuthenticationContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, AuthenticationContext.SCHEMA).CommandTimeout(30))
    .LogTo(Console.WriteLine));

// Add services to the container.
#region Services
builder.Services.AddScoped<SecretOptions>();
builder.Services.AddScoped<DbContext, AuthenticationContext>();
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IPartnerRepository, PartnerRepository>();
builder.Services.AddTransient<IOrganisationRepository, OrganisationRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenService, TokenService>();
#endregion

//builder.Services.AddAsymmetricAuthentication();

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
