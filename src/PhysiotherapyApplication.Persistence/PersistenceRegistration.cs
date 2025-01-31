using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.Services;
using PhysiotherapyApplication.Persistence.Contexts;
using PhysiotherapyApplication.Persistence.Options;
using PhysiotherapyApplication.Persistence.Repositories;
using PhysiotherapyApplication.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Persistence.Services.AppointmentService;
using PhysiotherapyApplication.Persistence.UnitOfWork;

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

        services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork,PhysiotherapyApplication.Persistence.UnitOfWork.UnitOfWork>();

        services.AddScoped<IAppointmentRepository,AppointmentRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IMedicalHistoryRepository, MedicalHistoryRepository>();
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        services.AddScoped<ITreatmentRepository,TreatmentRepository>();
        services.AddScoped<IDoctorDetailRepository,DoctorDetailRepository>();
        services.AddScoped<IPatientDetailRepository,PatientDetailRepository>();

        // Services
        services.AddScoped<IAppointmentService,AppointmentService>();

        

        return services;
    }
}
