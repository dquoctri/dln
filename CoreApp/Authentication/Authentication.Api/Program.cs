
using Authentication.Api.Extensions;
using Authentication.Api.Models;
using Authentication.Api.Services;
using User.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
var secretOptions = builder.Configuration.GetSection(SecretOptions.CONFIG_KEY);
var serect = secretOptions.Get<SecretOptions>();
builder.Services.Configure<SecretOptions>(secretOptions);

// Add services to the container.
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<SecretOptions>();

builder.Services.AddRefreshAuthentication(serect);

builder.Services.AddAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
