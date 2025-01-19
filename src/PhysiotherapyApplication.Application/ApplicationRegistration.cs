using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PhysiotherapyApplication.Application.Mapper;

namespace PhysiotherapyApplication.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}
