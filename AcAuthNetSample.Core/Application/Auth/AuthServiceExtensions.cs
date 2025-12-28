using AcAuthNetSample.Core.Application.Auth.Interfaces;
using AcAuthNetSample.Core.Application.Auth.Services;
using AcAuthNetSample.Core.Application.Configuration.Options;
using AcAuthNetSample.Core.Domain.Auth.Interfaces;
using AcAuthNetSample.Core.Domain.Auth.Services;
using AcAuthNetSample.Core.Infrastructure.Repositories.Auth;
using AcAuthNetSample.Core.Infrastructure.Repositories.Auth.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth {
    internal static class AuthServiceExtensions {

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserRegistractionService, UserRegistrationService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccessTokenRepository, AccessTokenRepository>();

            // Jwt注册
            services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);
            services.AddOptions<JwtConfigOptions>()
                .Bind(configuration.GetSection(JwtConfigOptions.Name))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options => {
                var jwtConfig = configuration.GetSection(JwtConfigOptions.Name).Get<JwtConfigOptions>()!;

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidTypes = [JwtConstants.HeaderType],
                    ValidAlgorithms = [SecurityAlgorithms.Sha256, SecurityAlgorithms.Sha384, SecurityAlgorithms.RsaSha256],
                    ValidIssuer = jwtConfig.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = jwtConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = jwtConfig.SymmetricSecurityKey,
                    ClockSkew = TimeSpan.FromSeconds(5), // 允许过期时间范围
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = ClaimTypes.Name
                };

                options.SaveToken = true;
            });
            services.AddScoped<IAuthTokenService, AuthTokenService>();

            return services;
        }
    }
}
