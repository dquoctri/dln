using Microsoft.EntityFrameworkCore;
using Repository.Common;
using User.Api.Extensions;
using User.Context;
using User.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Repositories
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IUserRepository, UserRepository>();
//builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
#endregion
builder.Services.AddDbContext<UserContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly(typeof(UserContext).Assembly.FullName)));

builder.Services.AddAsymmetricAuthentication();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfiguringSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
