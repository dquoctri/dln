using Authentication.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Authentication.Api.Extensions
{
    public static class RefreshAuthenticationExtensions
    {
        public static IServiceCollection AddRefreshAuthentication(this IServiceCollection services, SecretSettings secret)
        {
            var refreshSecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret.RefreshSecretKey));
            var signingCredentials = new SigningCredentials(refreshSecretKey, SecurityAlgorithms.HmacSha512Signature);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = secret.Issuer,
                        ValidAudience = secret.Audience,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        IssuerSigningKey = refreshSecretKey,
                        ValidateLifetime = true,
                        LifetimeValidator = LifetimeValidator
                    };
                });

            return services;
        }

        private static bool LifetimeValidator(DateTime? notBefore,
            DateTime? expires,
            SecurityToken securityToken,
            TokenValidationParameters validationParameters)
        {
            return expires != null && expires > DateTime.UtcNow;
        }
    }
}
