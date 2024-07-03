using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UBC.Core.Data.Context;
using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Models;

namespace UBC.Core.WebApi.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<MeuContexto>(options =>
                             options.UseInMemoryDatabase("TestConnection"));
        }

        #region IdentityConfiguration

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseInMemoryDatabase("AspNetCoreIdentityContextConnection"));

            services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters = "aãâàábcçdeéêfgğhiíîìjklmnoöõôópqrsştuüúûùvwxyzAÃÂÀÁBCÇDEÉÊFGĞHIİÎÌJKLMNOÖÔÕÓPQRSŞTUÜÚÛÙVWXYZ0123456789-._@+/ ";
                options.Tokens.PasswordResetTokenProvider = "7DaysToken";
            })
               .AddDefaultUI()
               // .AddErrorDescriber<IdentityMensagensPortugues>()
               .AddEntityFrameworkStores<IdentityContext>()
               .AddDefaultTokenProviders()
               .AddTokenProvider<DataProtectorTokenProvider<UserEntity>>("7DaysToken");

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromDays(7);
                opt.Name = "7DaysToken";
            });

            // JWT
            var appSettingsSection = configuration.GetSection("AuthorizationSettings");
            services.Configure<AuthorizationSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AuthorizationSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidOn,
                    ValidIssuer = appSettings.Issuer,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }

        #endregion
    }
}
