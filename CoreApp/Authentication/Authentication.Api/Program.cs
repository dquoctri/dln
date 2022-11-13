
using Authentication.Api.Extensions;
using Authentication.Api.Models;
using Authentication.Api.Services;
using Authentication.Context;
using Authentication.Repository;
using Authentication.Repository.Architectures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Repository.Common;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);

        var secretOptions = builder.Configuration.GetSection(SecretSettings.CONFIG_KEY);
        SecretSettings serect = secretOptions.Get<SecretSettings>() ?? throw new ArgumentNullException();
        builder.Services.Configure<SecretSettings>(secretOptions);

        builder.Services.AddDbContext<AuthenticationContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, AuthenticationContext.SCHEMA).CommandTimeout(30));
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                options.LogTo(Console.WriteLine);
            }

        }, ServiceLifetime.Transient);

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetSection("Redis")["RedisConnection"];
            options.InstanceName = "AuthenticationCache";
        });
        //builder.Services.AddDistributedMemoryCache();


        // Add services to the container.
        #region Services
        builder.Services.AddSingleton<SecretSettings>();
        builder.Services.AddScoped<DbContext, AuthenticationContext>();
        builder.Services.AddTransient<IPartnerRepository, PartnerRepository>();
        builder.Services.AddTransient<IOrganizerRepository, OrganizerRepository>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IAccountRepository, AccountRepository>();
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        #endregion

        //builder.Services.AddAsymmetricAuthentication();
        builder.Services.AddRefreshAuthentication(serect);
        builder.Services.AddAuthorization();

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddConfiguringSwagger();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddResponseCaching();

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
            c.DisplayRequestDuration();
            c.DocExpansion(DocExpansion.None);
            c.EnableDeepLinking();
            c.EnableFilter();
            c.MaxDisplayedTags(5);
            c.ShowExtensions();
            c.ShowCommonExtensions();
            c.EnableValidator();
            c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete);

        });

        app.UseResponseCaching();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

#region Services

#endregion
