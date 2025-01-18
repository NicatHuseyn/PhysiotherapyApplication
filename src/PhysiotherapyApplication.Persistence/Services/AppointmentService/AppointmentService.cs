using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.Services;
using PhysiotherapyApplication.Application.Paging;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Persistence.Services.AppointmentService;

public class AppointmentService(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork) : IAppointmentService
{
    public async Task<ServiceResult<Appointment>> AddAsync(CreateAppointmentDto model)
    {
        var result = await appointmentRepository.AddAsync(new Appointment
        {
            AppointmentDateTime = model.AppointmentDateTime,
            Status = model.AppointmentStatus,
            CancellationReason = model.CancellationReason,
            ConsultationFee = model.ConsultationFee,
            IsPaid = model.isPaid,
            Notes = model.Notes,
            PatientId = model.PatientId,
            TreatmentId = model.TreatmentId,
            Duration = model.Duration
        });

        await unitOfWork.CommitAsync();

        return ServiceResult<Appointment>.Success(result);
    }

    public Task<ServiceResult<ICollection<CreateAppointmentDto>>> AddRangeAsync(ICollection<CreateAppointmentDto> entities)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<bool>> AnyAsync(Expression<Func<AppointmentDto, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<AppointmentDto>> DeleteAsync(AppointmentDto model, bool permanent = false)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<ICollection<AppointmentDto>>> DeleteRangeAsync(ICollection<AppointmentDto> entities, bool permanent = false)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<AppointmentDto>> GetAsync(Expression<Func<AppointmentDto, bool>> predicate, Func<IQueryable<AppointmentDto>, IIncludableQueryable<AppointmentDto, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<Pagination<AppointmentDto>>> GetListAsync(Expression<Func<AppointmentDto, bool>> predicate, Func<IQueryable<AppointmentDto>, IOrderedQueryable<AppointmentDto>>? orderBy = null, Func<IQueryable<AppointmentDto>, IIncludableQueryable<AppointmentDto, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ServiceResult<UpdateAppointmentDto> Udpated(UpdateAppointmentDto model)
    {
        throw new NotImplementedException();
    }

    public ServiceResult<ICollection<UpdateAppointmentDto>> UpdateRange(ICollection<UpdateAppointmentDto> entities)
    {
        throw new NotImplementedException();
    }
}
