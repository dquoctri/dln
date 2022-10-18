using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Uzer.Context;

namespace Uzer.Api.Extensions
{
    public static class AddDbContextExtentions
    {
        //public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static IServiceCollection AddDefaultDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString,
                x => x.MigrationsAssembly(typeof(UserContext).FullName)
                    .MigrationsHistoryTable(HistoryRepository.DefaultTableName, UserContext.SCHEMA)));

            return services;

            //private readonly StreamWriter _logStream = new StreamWriter("mylog.txt", append: true);
            //todo unitest cannot access here
            //view more https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/simple-logging
            //optionsBuilder.LogTo(_logStream.WriteLine, LogLevel.Information).EnableDetailedErrors();
        }
    }
}
