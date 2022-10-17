using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Uzer.Api.Extensions
{
    public static class ConfiguringSwaggerExtensions
    {
        public static IServiceCollection AddConfiguringSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Uzer API",
                    Description = "An ASP.NET Core Web API for managing Uzer API",
                    TermsOfService = new Uri("https://dqtri.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Deadl!ne Contact",
                        Url = new Uri("https://dqtri.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Deadl!ne License",
                        Url = new Uri("https://dqtri.com/license")
                    }
                });
                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a refresh token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                setupAction.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }

        public static IApplicationBuilder UseCustomSwaggerUI(this IApplicationBuilder app, string routePrefix)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = routePrefix + "/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/{routePrefix}/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = $"{routePrefix}/swagger";
            });
            return app;
        }
    }
}
