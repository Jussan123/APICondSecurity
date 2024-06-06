using APICondSecurity.Account;
using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Identity;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Repositories;
using APICondSecurity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace APICondSecurity.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddDbContext<condSecurityContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(condSecurityContext).Assembly.FullName));
            });
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["jwt:issuer"],
                    ValidAudience = configuration["jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["jwt:secretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            Services.AddAutoMapper(typeof(EntitiesToDTOMappingProfile));
            //repositories
            Services.AddScoped<IUser, UserRepository>();

            //services
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<IAuthenticate, AuthenticateService>();
            return Services;
        }
    }
}
