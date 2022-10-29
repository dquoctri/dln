using Authentication.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authentication.Api.Extensions
{
    public static class AddDbContextExtentions
    {
        public static IServiceCollection AddDefaultDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(connectionString,
                x => x.MigrationsAssembly(typeof(AuthenticationContext).FullName)
                    .MigrationsHistoryTable(HistoryRepository.DefaultTableName, AuthenticationContext.SCHEMA)));
            return services;
        }
    }
}
