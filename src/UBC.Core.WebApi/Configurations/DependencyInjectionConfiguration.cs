using UBC.Core.Data.Context;
using UBC.Core.Data.Repository;
using UBC.Core.Domain.Interfaces.Notifications;
using UBC.Core.Domain.Interfaces.Repositories;
using UBC.Core.Domain.Interfaces.Services;
using UBC.Core.Domain.Interfaces.Services.Identity;
using UBC.Core.Domain.Notifications;
using UBC.Core.Service.Application;
using UBC.Core.Service.Facades;
using UBC.Core.Service.Facades.Identity;
using UBC.Core.Service.Facades.Interfaces;

namespace UBC.Core.WebApi.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Lifestyle.Transient => Uma instância para cada solicitação
            // Lifestyle.Singleton => Uma instância única para a classe (para servidor)
            // Lifestyle.Scoped => Uma instância única para o request

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<INotificador, Notifier>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            #region Application - Facade

            services.AddScoped<IStudentFacade, StudentFacade>();

            #region UserIdentity

            //  services.AddTransient<ILoginRegisterUserIdentityFacade, LoginRegisterUserIdentityFacade>();
            //services.AddTransient<IUserIdentityFacade, UserIdentityFacade>();
            //services.AddTransient<IUserIdentityClaimFacade, UserIdentityClaimFacade>();

            #endregion

            #endregion

            #region Domain

            services.AddScoped<IStudentAppService, StudentAppService>();

            #region UserIdentity

            // services.AddTransient<ILoginRegisterUserIdentityAppService, LoginRegisterUserIdentityAppService>();
            //services.AddTransient<IUserIdentityAppService, UserIdentityAppService>();
            //services.AddTransient<IUserIdentityClaimAppService, UserIdentityClaimAppService>();
            services.AddScoped<IUserAppService, UserAppService>();
                      
            #endregion

            #endregion

            #region InfraData

            services.AddScoped<MeuContexto>();
            // services.AddScoped<IdentityContext>();

            services.AddScoped<IStudentRepository, StudentRepository>();

            #region UserIdentity

            //services.AddTransient<IUserIdentityRepository, UserIdentityRepository>();
            //services.AddTransient<IUserIdentityClaimRepository, UserIdentityClaimRepository>();

            #endregion

            #endregion
        }
    }
}
