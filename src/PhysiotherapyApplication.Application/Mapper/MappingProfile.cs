using System.Reflection;
using AutoMapper;

namespace PhysiotherapyApplication.Application.Mapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        // Constructor calls a method to apply mappings from the current assembly
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        // Find all public, non-abstract classes that inherit from Profile
        var types =  assembly.GetExportedTypes()
            .Where(t=>t.IsClass && !t.IsAbstract && typeof(Profile).IsAssignableFrom(t));

        foreach (var type in types)
        {
            // Create an instance of each found Profile class
            var profile = (Profile)Activator.CreateInstance(type);   
        }
    }
}
