using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace PhysiotherapyApplication.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        #region Mapster Configurations
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        #endregion

        #region FluentValidation Configurations
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        #endregion

        #region Mediator Configurations
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        #endregion


        return services;
    }
}
