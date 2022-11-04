
using Microsoft.EntityFrameworkCore;
using Authentication.Api.Extensions;
using Authentication.Api.Models;
using Authentication.Api.Services;
using Authentication.Context;
using Microsoft.EntityFrameworkCore.Migrations;
using Repository.Common;
using Authentication.Repository;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);

var secretOptions = builder.Configuration.GetSection(SecretOptions.CONFIG_KEY);
var serect = secretOptions.Get<SecretOptions>();
builder.Services.Configure<SecretOptions>(secretOptions);

builder.Services.AddDbContext<AuthenticationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, AuthenticationContext.SCHEMA).CommandTimeout(30));
    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
    {
        options.LogTo(Console.WriteLine);
    }
    
}, ServiceLifetime.Transient);

// Add services to the container.
#region Services
builder.Services.AddSingleton<SecretOptions>();
builder.Services.AddTransient<DbContext, AuthenticationContext>();
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

builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
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
app.UseSwaggerUI(c =>
{
    c.DefaultModelExpandDepth(2);
    c.DefaultModelRendering(ModelRendering.Example);
    c.DefaultModelsExpandDepth(-1);
    c.DisplayOperationId();
    c.DisplayRequestDuration();
    c.DocExpansion(DocExpansion.None);
    c.EnableDeepLinking();
    c.EnableFilter();
    c.MaxDisplayedTags(5);
    c.ShowExtensions();
    c.ShowCommonExtensions();
    c.EnableValidator();
    c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete);
    c.UseRequestInterceptor("(request) => { return request; }");
    c.UseResponseInterceptor("(response) => { return response; }");
    
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
