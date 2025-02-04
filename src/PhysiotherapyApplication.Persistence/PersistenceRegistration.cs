using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhysiotherapyApplication.Application;
using PhysiotherapyApplication.Domain.Options;
using PhysiotherapyApplication.Persistence.Contexts;
using PhysiotherapyApplication.Persistence.Interceptors;
using PhysiotherapyApplication.WebApi.Filters;
using Scrutor;

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

            options.AddInterceptors(new DbSaveChangesInterceptor());
        });

        #endregion

        services.Scan(scan => scan
        .FromAssemblies(typeof(ApplicationAssembly).Assembly, typeof(PersistenceAssembly).Assembly)
        .AddClasses(publicOnly: false)
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsMatchingInterface()
        .WithScopedLifetime()

        .AddClasses(classes=>classes.AssignableTo(typeof(NotFoundFilter<>)))
        .AsSelf()
        .WithScopedLifetime()
        );

        // Closed .NET Default messages
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        return services;
    }
}
