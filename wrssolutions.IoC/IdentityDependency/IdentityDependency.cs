using wrssolutions.Configs;
using wrssolutions.Data.Entity;
using wrssolutions.Domain.Entities.Auth;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace wrssolutions.IoC.Identity
{
    public static class IdentityDependency
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            Settings.JwtValidate = configuration["KeyVault:EndPoint"]!;

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration["KeyVault:DefaultConnection"], providerOptions => providerOptions.EnableRetryOnFailure()));

            services.Configure<DataProtectionTokenProviderOptions>(
            x => x.TokenLifespan = TimeSpan.FromMinutes(20));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            //should required by Regex FrontEnd
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddTransient<IProfileService, IdentityWithAdditionalClaimsProfileService>();

            services.AddAntiforgery(options =>
            {
                options.SuppressXFrameOptionsHeader = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddAuthorization();

            //JWT
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.JwtSecret));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Settings.JwtValidate,
                    ValidIssuer = Settings.JwtEmissor
                };
            });

            return services;
        }
    }
}
