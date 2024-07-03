using Microsoft.EntityFrameworkCore;
using UBC.Core.Data.Context;

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
    }
}
