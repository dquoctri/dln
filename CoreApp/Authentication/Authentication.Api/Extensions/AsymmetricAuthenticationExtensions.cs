using Authentication.Api.Certificates;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Api.Extensions
{
    public static class AsymmetricAuthenticationExtensions
    {
        public static IServiceCollection AddAsymmetricAuthentication(this IServiceCollection services)
        {
            var issuerSigningCertificate = new SigningIssuerCertificate();
            RsaSecurityKey issuerSigningKey = issuerSigningCertificate.GetIssuerSigningKey();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = issuerSigningKey,
                    ValidateLifetime = true,
                    LifetimeValidator = (
                        DateTime? notBefore,
                        DateTime? expires,
                        SecurityToken securityToken,
                        TokenValidationParameters validationParameters
                    ) => expires != null && expires > DateTime.UtcNow
                };
            });

            return services;
        }
    }
}
