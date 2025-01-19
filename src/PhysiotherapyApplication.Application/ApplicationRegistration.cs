using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PhysiotherapyApplication.Application.Mapper;

namespace PhysiotherapyApplication.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        #region AutoMapper Configurations
        services.AddAutoMapper(typeof(MappingProfile));
        #endregion

        #region FluentValidation Configurations
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        #endregion


        return services;
    }
}
