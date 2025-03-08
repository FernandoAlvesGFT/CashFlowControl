using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CashFlowControl.Core.Infrastructure.Configurations.ResolveDI
{
    public static class TokenJwtDI
    {
        public static void RegistryGenerate(WebApplicationBuilder builder)
        {
            var validIssuer = builder.Configuration["Jwt:Issuer"] ?? string.Empty;
            var IssuerSigningKey = builder.Configuration["Jwt:SecretKey"] ?? string.Empty;

            if (string.IsNullOrEmpty(validIssuer) || string.IsNullOrEmpty(IssuerSigningKey))
            {
                throw new InvalidOperationException("JWT settings not defined.");
            }

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer("Bearer", options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = validIssuer,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKey))
                     };
                 });
        }

        public static void RegistryConsumer(WebApplicationBuilder builder)
        {
            var validIssuer = builder.Configuration["Jwt:Issuer"] ?? string.Empty;
            var validAudience = builder.Configuration["Jwt:Audience"] ?? string.Empty;
            var IssuerSigningKey = builder.Configuration["Jwt:SecretKey"] ?? string.Empty;

            var urlApiAuth = builder.Configuration["urlApiAuth"] ?? string.Empty;

            if (string.IsNullOrEmpty(validIssuer) || string.IsNullOrEmpty(validAudience) || string.IsNullOrEmpty(IssuerSigningKey) || string.IsNullOrEmpty(urlApiAuth))
            {
                throw new InvalidOperationException("JWT settings not defined.");
            }

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer("Bearer", options =>
                 {
                     options.SaveToken = true;
                     options.Authority = urlApiAuth; 
                     options.RequireHttpsMetadata = true;
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = validIssuer,
                         ValidAudience = validAudience,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKey))
                     };
                 });

            builder.Services.AddAuthorization();
        }
    }
}
