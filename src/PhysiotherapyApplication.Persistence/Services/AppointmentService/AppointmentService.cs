using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.Services;
using PhysiotherapyApplication.Application.Paging;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Persistence.Services.AppointmentService;

public class AppointmentService(IAppointmentRepository appointmentRepository) : IAppointmentService
{
    public async Task<ServiceResult<Appointment>> AddAsync(Appointment model)
    {
        var result = await appointmentRepository.AddAsync(model);

        return ServiceResult<Appointment>.Success(result);
    }

    public async Task<ServiceResult<ICollection<Appointment>>> AddRangeAsync(ICollection<Appointment> entities)
    {
        var result = await appointmentRepository.AddRangeAsync(entities);
        return ServiceResult<ICollection<Appointment>>.Success(result);
    }

    public async Task<ServiceResult<bool>> AnyAsync(Expression<Func<Appointment, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
       var result = await appointmentRepository.AnyAsync(predicate,withDeleted,enableTracking,cancellationToken);

        return ServiceResult<bool>.Success(result);
    }

    public async Task<ServiceResult<Appointment>> DeleteAsync(Appointment model, bool permanent = false)
    {
        var result = await appointmentRepository.DeleteAsync(model, permanent);
        return ServiceResult<Appointment>.Success(result);
    }

    public async Task<ServiceResult<ICollection<Appointment>>> DeleteRangeAsync(ICollection<Appointment> entities, bool permanent = false)
    {
        var result = await appointmentRepository.DeleteRangeAsync(entities, permanent);
        return ServiceResult<ICollection<Appointment>>.Success(result);
    }

    public async Task<ServiceResult<Appointment>> GetAsync(Expression<Func<Appointment, bool>> predicate, Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        var result = await appointmentRepository.GetAsync(predicate,include,withDeleted,enableTracking, cancellationToken);

        return ServiceResult<Appointment>.Success(result);
    }

    public async Task<ServiceResult<Pagination<Appointment>>> GetListAsync(Expression<Func<Appointment, bool>> predicate, Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>>? orderBy = null, Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        var result = await appointmentRepository.GetListAsync(predicate,orderBy,include,index,size,withDeleted,enableTracking,cancellationToken);

        return ServiceResult<Pagination<Appointment>>.Success(result);
    }

    public ServiceResult<Appointment> Udpated(Appointment model)
    {
        var result = appointmentRepository.Udpated(model);
        return ServiceResult<Appointment>.Success(result);
    }

    public ServiceResult<ICollection<Appointment>> UpdateRange(ICollection<Appointment> entities)
    {
        var result = appointmentRepository.UpdateRange(entities);
        return ServiceResult<ICollection<Appointment>>.Success(result);
    }
}
