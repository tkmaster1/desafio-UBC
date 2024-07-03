using UBC.Core.Data.Context;
using UBC.Core.Data.Repository;
using UBC.Core.Data.Repository.Identity;
using UBC.Core.Domain.Interfaces.Notifications;
using UBC.Core.Domain.Interfaces.Repositories;
using UBC.Core.Domain.Interfaces.Repositories.Identity;
using UBC.Core.Domain.Interfaces.Services;
using UBC.Core.Domain.Interfaces.Services.Identity;
using UBC.Core.Domain.Notifications;
using UBC.Core.Service.Application;
using UBC.Core.Service.Application.Identity;
using UBC.Core.Service.Facades;
using UBC.Core.Service.Facades.Identity;
using UBC.Core.Service.Facades.Interfaces;
using UBC.Core.Service.Facades.Interfaces.Identity;

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

            services.AddTransient<ILoginRegisterUserFacade, LoginRegisterUserFacade>();
            services.AddTransient<IUserFacade, UserFacade>();
            services.AddScoped<IStudentFacade, StudentFacade>();

            #endregion

            #region Domain

            services.AddScoped<IUserLoginAppService, UserLoginAppService>();
            services.AddTransient<ILoginRegisterUserAppService, LoginRegisterUserAppService>();
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddScoped<IStudentAppService, StudentAppService>();

            #endregion

            #region InfraData

            services.AddScoped<MeuContexto>();
            services.AddScoped<IdentityContext>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();

            #endregion
        }
    }
}
