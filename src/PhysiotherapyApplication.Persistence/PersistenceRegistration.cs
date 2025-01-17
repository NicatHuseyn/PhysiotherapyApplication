using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhysiotherapyApplication.Persistence.Contexts;
using PhysiotherapyApplication.Persistence.Options;

namespace PhysiotherapyApplication.Persistence;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
    {
        #region Database Configurations

        var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

        services.AddDbContext<PhysiotherapyApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString!.SqlServer, sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
            });
        });

        #endregion

        return services;
    }
}
